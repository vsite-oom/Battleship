namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private List<Square> squares;
        private readonly IEnumerable<int> shipLengths;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
