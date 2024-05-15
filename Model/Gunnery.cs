using System.Diagnostics;

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
            this.shipLengths = shipLengths;
        }

        public Square Next()
        {
            target=targetSelector.Next();
            return target;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            RecordTargetResult(hitResult);

            switch (hitResult)
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
                            ChangeTacticsToInline(); break;
                        case ShootingTactics.Inline:
                            return;
                        default:
                            Debug.Assert(false);
                            return;
                    }
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
                recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Eliminated);
            }
            shipSquares.Clear();
        }

        private void RecordTargetResult
        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
        }

        private void ChangeTacticsToSurrounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
        }

        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Inline;
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        private readonly ShotsGrid recordGrid;

        private ITargetSelector targetSelector = new RandomTargetSelector();

        private List<squares>shipSquares=new List<Square>();

        private readonly SquareEliminator eliminator= new SquareEliminator();
    }
}