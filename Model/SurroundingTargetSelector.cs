
namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength) {
        this.grid = grid;
        this.firstHit = firstHit;
        this.shipLength = shipLength;
    }

        private readonly ShotsGrid grid;
        private readonly Square firstHit;
        private readonly int shipLength;
        public Square Next()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            var up = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Upwards);
            if (up.Count() > 0)
            {
                squares.Add(up);
            }
            var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Rightwards);
            if (right.Count() > 0)
            {
                squares.Add(right);
            }
            var Down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Downards);
            if (Down.Count() > 0)
            {
                squares.Add(Down);
            }

            var Left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Leftwards);
            if (Left.Count() > 0)
            {
            squares.Add(Left); 
            }

            throw new NotImplementedException();
        }
    }
}
