namespace Vsite.Oom.Battleship.Model;

public class FleetGrid(int rows, int columns) : Grid(rows, columns)
{
    public override IEnumerable<Square> Squares
    {
        get { return _squares.Cast<Square>().Where(x => x != null); }
    }

    public override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column] != null;
    }

    public void EliminateSquare(int row, int column)
    {
        _squares[row, column] = null;
    }
}