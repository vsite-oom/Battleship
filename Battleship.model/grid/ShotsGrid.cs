namespace Battleship.Model;

/// <summary>Specifies the direction in which to scan from a grid square.</summary>
public enum Direction
{
    /// <summary>Scan upwards (decreasing row index).</summary>
    Upwards,
    /// <summary>Scan rightwards (increasing column index).</summary>
    Rightwards,
    /// <summary>Scan downwards (increasing row index).</summary>
    Downwards,
    /// <summary>Scan leftwards (decreasing column index).</summary>
    Leftwards
}

/// <summary>Represents the grid used to record shots fired during the game.</summary>
public class ShotsGrid : Grid
{
    /// <summary>Initializes a new instance of the <see cref="ShotsGrid"/> class.</summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    public ShotsGrid(int rows, int columns) : base(rows, columns)
    {
    }

    /// <inheritdoc/>
    protected override bool IsSquareAvailable(int row, int column)
    {
        return squares[row, column]?.SquareState == SquareState.Intact;
    }

    /// <summary>Updates the state of the square at the given position.</summary>
    /// <param name="row">The row index of the square.</param>
    /// <param name="column">The column index of the square.</param>
    /// <param name="newState">One of the enumeration values that specifies the new state.</param>
    public void ChangeSquareState(int row, int column, SquareState newState)
    {
        squares[row, column]!.ChangeState(newState);
    }

    /// <summary>Returns the consecutive available squares starting adjacent to the given position in the specified direction.</summary>
    /// <param name="row">The row index of the starting position (exclusive).</param>
    /// <param name="column">The column index of the starting position (exclusive).</param>
    /// <param name="direction">One of the enumeration values that specifies the direction to scan.</param>
    /// <returns>The sequence of available squares in the given direction, stopping at the grid boundary or an unavailable square.</returns>
    public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction direction)
    {
        var result = new List<Square>();

        var (deltaRow, deltaColumn) = direction switch
        {
            Direction.Upwards    => (-1,  0),
            Direction.Rightwards => ( 0, +1),
            Direction.Downwards  => (+1,  0),
            Direction.Leftwards  => ( 0, -1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        for (int r = row + deltaRow, c = column + deltaColumn;
             r >= 0 && r < Rows && c >= 0 && c < Columns && IsSquareAvailable(r, c);
             r += deltaRow, c += deltaColumn)
        {
            result.Add(squares[r, c]!);
        }
        return result;
    }
}
