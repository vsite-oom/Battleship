namespace Vsite.Oom.Battleship.Model
{
    public class FleetGrid : Grid
    {
        public FleetGrid(int rows, int columns) : base(rows, columns)
        {
        }

        public override IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square?>().Where(s => s != null).Cast<Square>(); }
        }

        public void EliminateSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public override bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column] != null;
        }
    }
}