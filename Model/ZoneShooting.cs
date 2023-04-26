namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstSquareHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            firstHit = firstSquareHit;
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private readonly Square firstHit;
        private readonly IEnumerable<int> shipLengths;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
