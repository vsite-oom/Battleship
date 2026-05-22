using Battleship.Model;

namespace Battleship.Model;

/// <summary>Represents the grid used to place ships before the game starts.</summary>
/// <remarks>
/// Squares can be eliminated from the grid to prevent ships from being placed adjacent to each other.
/// </remarks>
public class FleetGrid : Grid
{
    /// <summary>Initializes a new instance of the <see cref="FleetGrid"/> class.</summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    public FleetGrid(int rows, int columns) : base(rows, columns)
    {
    }

    /// <summary>Gets all non-eliminated squares in the grid.</summary>
    public override IEnumerable<Square> Squares => squares.OfType<Square>();

    /// <summary>Removes the square at the given position, making it unavailable for ship placement.</summary>
    /// <param name="row">The row index of the square to eliminate.</param>
    /// <param name="column">The column index of the square to eliminate.</param>
    public void EliminateSquare(int row, int column)
    {
        squares[row, column] = null;
    }

    /// <inheritdoc/>
    protected override bool IsSquareAvailable(int row, int column)
    {
        return squares[row, column] != null;
    }
}