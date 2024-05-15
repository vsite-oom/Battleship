namespace Vsite.Oom.Battleship.Model;

public class ShotsGrid(int rows, int columns) : Grid(rows, columns)
{
    public override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column]?.SquareState == SquareState.Intact;
    }

    public Square GetSquare(int row, int column)
    {
        return _squares[row, column]!;
    }
}