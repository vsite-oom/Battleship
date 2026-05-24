namespace Battleship.Model;

/// <summary>Calculates the squares that must be eliminated around a placed ship to enforce spacing rules.</summary>
public class SquareEliminator
{
    /// <summary>Returns the coordinates of all squares to eliminate around the given ship squares.</summary>
    /// <param name="shipSquares">The squares occupied by the ship.</param>
    /// <param name="rows">The total number of rows in the grid.</param>
    /// <param name="columns">The total number of columns in the grid.</param>
    /// <returns>The coordinates of squares to eliminate, including the ship squares themselves and all adjacent squares.</returns>
    public IEnumerable<SquareCoordinate> ToEliminate(IEnumerable<Square> shipSquares, int rows, int columns)
    {
        var first = shipSquares.First();
        int firstRow = Math.Max(0, first.Row - 1);
        int firstColumn = Math.Max(0, first.Column - 1);

        var last = shipSquares.Last();
        int lastRow = Math.Min(rows - 1, last.Row + 1);
        int lastColumn = Math.Min(columns - 1, last.Column + 1);

        var result = new List<SquareCoordinate>();
        for (int r = firstRow; r <= lastRow; ++r)
        {
            for (int c = firstColumn; c <= lastColumn; ++c)
            {
                result.Add(new SquareCoordinate(r, c));
            }
        }
        return result;
    }
}