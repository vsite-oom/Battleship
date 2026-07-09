namespace Model;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class ShotsGrid : Grid
{
    public ShotsGrid(int rows, int columns) : base(rows, columns)
    {
    }

    public void ChangeSquareState(int row, int column, SquareState state)
    {
        _squares[row, column]?.ChangeState(state);
    }

    protected override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column] != null && _squares[row, column]!.State == SquareState.None;
    }

    public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction direction)
    {
        var result = new List<Square>();

        var (dr, dc) = direction switch
        {
            Direction.Up    => (-1,  0),
            Direction.Right => ( 0, +1),
            Direction.Down  => (+1,  0),
            Direction.Left  => ( 0, -1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        for (int r = row + dr, c = column + dc;
             r >= 0 && r < Rows && c >= 0 && c < Columns && IsSquareAvailable(r, c);
             r += dr, c += dc)
        {
            result.Add(_squares[r, c]!);
        }

        return result;
    }
}
