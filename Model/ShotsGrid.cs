
namespace Vsite.Oom.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Rightwards,
        Downards,
        Leftwards,
    }
    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column]?.SquareState == SquareState.Intact;
        }

        public void ChangeSquareState(int row, int column, SquareState newState)
        {
            squares[row, column].ChangeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction direction)
        {
            throw new NotImplementedException(); // za zadacu
        }

    }
}
