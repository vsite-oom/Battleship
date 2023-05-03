namespace Vsite.Oom.Battleship.Model
{
    public enum CurrentShootingTactics
    {
        Random,
        Zone,
        Line
    }

    public class Gunnery
    {
       
        public Gunnery(GameRules gameRules)
        {
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            shootingTactics = new RandomShooting(grid);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square NextTarget()
        {
            return shootingTactics.NextTarget();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
             
        }

        private readonly Grid grid;
        private IShootingTactics shootingTactics;

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
