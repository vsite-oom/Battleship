namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        private readonly List<SquareCoordinate> surroundingCoordinates = new List<SquareCoordinate>();
        private int currentIndex = 0;

        public SurroundingTargetSelector(SquareCoordinate center)
        {
            int row = center.Row;
            int column = center.Column;

            surroundingCoordinates.Add(new SquareCoordinate(row - 1, column));
            surroundingCoordinates.Add(new SquareCoordinate(row + 1, column));
            surroundingCoordinates.Add(new SquareCoordinate(row, column - 1));
            surroundingCoordinates.Add(new SquareCoordinate(row, column + 1));
        }

        public SquareCoordinate Next()
        {
            if (currentIndex < surroundingCoordinates.Count)
            {
                LastHitCoordinate = surroundingCoordinates[currentIndex];
                currentIndex++;
                return LastHitCoordinate;
            }
            throw new InvalidOperationException("Nemate vise koordinata");
        }

        public SquareCoordinate LastHitCoordinate { get; private set; }
    }
}