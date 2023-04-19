namespace Vsite.Oom.Battleship.Model
{
    public class Gunnery
    {
        public enum CurrentShootingTactics
        {
            Random,
            Zone,
            Line
        }

        public Gunnery(GameRules gameRules, Fleet fleet)
        {
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
            currentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square nextTarget()
        {
            return shootingTactics.NextTarget();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            throw new NotImplementedException();
        }

        private readonly Grid grid;
        private readonly Fleet fleet;
        private IShootingTactics shootingTactics;

        public CurrentShootingTactics currentShootingTactics { get; private set; }
    }
}
