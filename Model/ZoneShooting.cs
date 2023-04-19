namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstHit)
        {
            this.grid = grid;
            this.firstHit = firstHit;
        }

        private readonly Grid grid;
        private readonly Square firstHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
