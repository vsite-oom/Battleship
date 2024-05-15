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
            recordGrid = new FleetGreed(rows, columns);
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
            targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
        }

        public Square Next()
        {
            target = targetSelector.Next();
            return target;
        }

        public void ProcessHitResult(HitResult hitResult)
        {

            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Hit:
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
                    ChangeTacticsToRandom();
                    return;
            }
        }

        private void ChangeTacticsToRandom()
        {
            ShootingTactics = ShootingTactics.Random;
            targetSelector = new RandomTargetSelector(recordGrid);
        }

        private void ChangeTacticsToSurrounding()
        {
            ShootingTactics = ShootingTactics.Surrounding;
            targetSelector = new SurroundingTargetSelector();
        }

        private void ChangeTacticsToInline()
        {
            ShootingTactics = ShootingTactics.Inline;
            targetSelector = new InlineTargetSelector();
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        private readonly FleetGreed recordGrid;
        private readonly List<int> shipLengths;
        private Square target;
        private ITargetSelector targetSelector = new RandomTargetSelector();
    }
}