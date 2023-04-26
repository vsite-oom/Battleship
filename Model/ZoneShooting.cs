namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private readonly IEnumerable<int> shipLengths;
        private readonly Square firstHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
