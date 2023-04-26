namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.shipLengths = shipLengths;
            this.squaresHit = new List<Square>(squaresHit);
        }

        private readonly Grid grid;
        private readonly IEnumerable<int> shipLengths;
        private List<Square> squaresHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
