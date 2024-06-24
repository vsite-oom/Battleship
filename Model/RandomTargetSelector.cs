namespace Vsite.Oom.Battleship.Model
{
    public class RandomTargetSelector : ITargetSelector
    {
        public RandomTargetSelector(ShotsGrid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
        }

        public Square Next()
        {
            var intactSquares = grid.Squares.Where(s => s.SquareState == SquareState.Intact).ToList();
            if (!intactSquares.Any())
            {
                throw new InvalidOperationException("No available targets to select.");
            }
            var selectedIndex = random.Next(intactSquares.Count());
            return intactSquares.ElementAt(selectedIndex);
        }

        private readonly ShotsGrid grid;
        private readonly int shipLength;
        private readonly Random random = new Random();
    }
}
