using System;
using System.Collections.Generic;
using System.Data;
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
        public void ProcessHitResult(HitResult hitResult)
        {
            RecordTargetResult(hitResult);

            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Surrounding;
                            targetSelector = new SurroundingTargetSelector();
                        }
                        break;
                    }
                case ShootingTactics.Surrounding:
                    {
                        if (hitResult == HitResult.Hit)
                        {
                            ShootingTactics = ShootingTactics.Inline;
                            targetSelector = new InlineTargetSelector();
                        }
                        else if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
                case ShootingTactics.Inline:
                    {
                        if (hitResult == HitResult.Sunken)
                        {
                            ShootingTactics = ShootingTactics.Random;
                            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
                        }
                        break;
                    }
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
                recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Eliminated);  // Ovo nije baš dobro (direktno možemo brljati po squareu), ali ostavit ćemo ovako.
            }
            shipSquares.Clear();
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;  // Initially it will be random.

        private readonly ShotsGrid recordGrid;

        private List<int> shipLengths = [];

        private List<Square> shipSquares = new List<Square>();

        private Square target;

        private ITargetSelector targetSelector;

        private readonly SquareEliminator eliminator = new SquareEliminator();
    }
}
