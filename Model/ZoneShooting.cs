namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstSquareHit)
        {
            this.grid = grid;
            firstHit = firstSquareHit;
        }

        private readonly Grid grid;
        private readonly Square firstHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
