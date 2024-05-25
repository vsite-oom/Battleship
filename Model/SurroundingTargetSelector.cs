namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength)
        {
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
            if (up.Any())
            {
                squares.Add(up);
            }

            var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Rightwards);
            if (right.Any())
            {
                squares.Add(right);
            }

            var down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Downards);
            if (down.Any())
            {
                squares.Add(down);
            }

            var left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Leftwards);
            if (left.Any())
            {
                squares.Add(left);
            }

            foreach (var directionSquares in squares)
            {
                foreach (var square in directionSquares)
                {
                    if (square.SquareState == SquareState.Intact)
                    {
                        return square;
                    }
                }
            }

            throw new InvalidOperationException("Nema vise meta");
        }
    }
}
