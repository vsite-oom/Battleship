using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }

    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new ShotsGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }

        public Square Next()
        {
            try
            {
                target = targetSelector.Next();
            }
            catch (InvalidOperationException)
            {
                ChangeTacticsToRandom();
                try
                {
                    target = targetSelector.Next();
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("No valid targets available.");
                }
            }
            return target;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    RecordTargetResult(hitResult);
                    return;
                case HitResult.Hit:
                    RecordTargetResult(hitResult);
                    switch (ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ChangeTacticsToSurrounding();
                            return;
                        case ShootingTactics.Surrounding:
                            ChangeTacticsToInline();
                            break;
                        case ShootingTactics.Inline:
                            return;
                        default:
                            Debug.Assert(false);
                            return;
                    }
                    return;
                case HitResult.Sunken:
                    RecordTargetResult(hitResult);
                    ChangeTacticsToRandom();
                    return;
            }
        }

        private void RecordTargetResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    target.ChangeState(SquareState.Missed);
                    return;
                case HitResult.Hit:
                    target.ChangeState(SquareState.Hit);
                    shipSquares.Add(target);
                    return;
                case HitResult.Sunken:
                    MarkShipSunken();
                    return;
            }
        }

        private void MarkShipSunken()
        {
            shipSquares.Add(target);
            foreach (var square in shipSquares)
            {
                square.ChangeState(SquareState.Sunken);
            }
            var toEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
            foreach (var square in toEliminate)
            {
                recordGrid.ChangeSquareState(square.Row, square.Column, SquareState.Eliminated);
            }
            shipSquares.Clear();
        }

        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
        }

        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Inline;
            targetSelector = new InlineTargetSelector(recordGrid, shipSquares, shipLengths[0]);
        }

        private void ChangeTacticsToSurrounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new SurroundingTargetSelector(recordGrid, target, shipLengths[0]);
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        private readonly ShotsGrid recordGrid;
        private readonly List<int> shipLengths;
        private List<Square> shipSquares = new List<Square>();
        private ITargetSelector targetSelector;
        private Square target;
        private readonly SquareEliminator eliminator = new SquareEliminator();
    }
}
