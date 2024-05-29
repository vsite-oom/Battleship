namespace Vsite.Oom.Battleship.Model;

public enum Direction
{
    Upwards,
    Downwards,
    Left,
    Right
}

public class ShotsGrid : Grid
{
    public ShotsGrid(int rows, int columns) : base(rows, columns) { }

    public override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column]?.SquareState == SquareState.Intact;
    }

    public Square GetSquare(int row, int column)
    {
        return _squares[row, column]!;
    }

    public void ChangeSquareState(int row, int column, SquareState newState)
    {
        _squares[row, column]!.ChangeState(newState);
    }

    public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction direction)
    {
        var result = new List<Square>();

        int tempRow = row;
        int tempCol = column;

        int rowDirection = direction switch
        {
            Direction.Upwards => -1,
            Direction.Downwards => 1,
            _ => 0
        };

        int colDirection = direction switch
        {
            Direction.Right => 1,
            Direction.Left => -1,
            _ => 0
        };

        while (tempRow > 0 && tempRow < this.Rows - 1 && tempCol > 0 && tempCol < this.Columns - 1)
        {
            tempRow += rowDirection;
            tempCol += colDirection;
            result.Add(new Square(tempRow, tempCol));
        }

        return result;

    }
}