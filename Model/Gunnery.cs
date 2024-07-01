using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace vsite.oom.battleship.model
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
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(s => s));
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }
        public Square Next()
        {
            target = targetSelector.Next();
            return target;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            RecordTargetResult(hitResult);
            switch(hitResult)
            {
                case HitResult.Missed:
                    target.ChangeState(SquareState.Missed);
                    return;
                case HitResult.Hit:
                    switch (ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ChangeTacticsToSurrounding();
                            return;
                        case ShootingTactics.Surrounding:
                            ChangeTacticsToInline();
                            return;
                        case ShootingTactics.Inline:
                            return;
                        default:
                            Debug.Assert(false);
                            return;
                    }
                case HitResult.Sunken:
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
                    MarkShipSunken(target);
                    return;
            }
        }

        private void MarkShipSunken(Square target)
        {
            shipSquares.Add(target);
            foreach (var square in shipSquares)
            {
                square.ChangeState(SquareState.Sunken);
            }
            eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
            foreach(var square in shipSquares)
            {
                recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Sunken);
            }
            shipSquares.Clear();
        }

        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
        }
        private void ChangeTacticsToSurrounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new SurroundingTargetSelector(recordGrid, target, shipLengths[0]);
        }
        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Inline;
            targetSelector = new InlineTargetSelector(recordGrid, shipSquares, shipLengths[0]);
        }
        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;
        private readonly ShotsGrid recordGrid;
        private List<Square> shipSquares = new List<Square>();
        private Square target;
        private readonly List<int> shipLengths;
        private ITargetSelector targetSelector;
        private readonly SquareEliminator eliminator = new SquareEliminator();
    }
}
