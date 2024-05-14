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
            recordGrid = new Grid(rows, columns);
            targetSelector = new RandomTargetSelector(rows, columns);
        }

        public SquareCoordinate Next()  
        {
            return targetSelector.Next();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            if (hitResult == HitResult.Missed)
            {
                return;
            }

            if (hitResult == HitResult.Hit)
            {
                if (ShootingTactics == ShootingTactics.Random)
                {
                    ShootingTactics = ShootingTactics.Surrounding;
                    targetSelector = new SurroundingTargetSelector(targetSelector.LastHitCoordinate);
                }
                else if (ShootingTactics == ShootingTactics.Surrounding)
                {
                    ShootingTactics = ShootingTactics.Inline;
                    targetSelector = new InlineTargetSelector(targetSelector.LastHitCoordinate);
                }
            }
            else if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                targetSelector = new RandomTargetSelector(recordGrid.Rows, recordGrid.Columns);
            }
        }

        public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

        private readonly Grid recordGrid;
        private ITargetSelector targetSelector;

    }
}
