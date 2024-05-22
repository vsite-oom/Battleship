using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly List<int> shipLengths = [];
        private readonly ShotsGrid recordGrid;
        private readonly SquareEliminator eliminator = new();
        private Square target;
        private ITargetSelector targetSelector;
        private List<Square> shipSquares = new();

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            recordGrid = new ShotsGrid(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderDescending());

            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }

        public Square Next()
        {
            target = targetSelector.Next();
            return target;
        }

        public void ProcessHit(HitResult hitResult)
        {
            RecordTargetResult(hitResult);

            if (hitResult == HitResult.Hit)
            {
                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ShootingTactics = ShootingTactics.Surrounding;
                        targetSelector = new SurroundingTargetSelector();
                        break;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        targetSelector = new InlineTargetSelector();
                        break;
                }
            }
            else if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
            }
        }

        private void RecordTargetResult(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    target.ChangeState(SquareState.Missed);
                    break;
                case HitResult.Hit:
                    target.ChangeState(SquareState.Hit);
                    shipSquares.Add(target);
                    break;
                case HitResult.Sunken:
                    MarkShipSunken();
                    break;
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
    }
}
