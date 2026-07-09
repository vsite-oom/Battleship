namespace Model;

public class FleetGrid : Grid
{
    public FleetGrid(int rows, int columns) : base(rows, columns)
    {
    }

    public override IEnumerable<Square> Squares => _squares.OfType<Square>();

    public void EliminateSquare(int row, int column)
    {
        _squares[row, column] = null;
    }

    protected override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column] != null;
    }
}
