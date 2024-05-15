namespace Vsite.Oom.Battleship.Model;

public class SquareEliminator
{
    public IEnumerable<SquareCoordinate> ToEliminate(IEnumerable<Square> shipSquares, int rows, int columns)
    {
        var first = shipSquares.First();
        var firstRow = first.Row;
        var firstColumn = first.Column;

        if (firstRow > 0)
            --firstRow;
        if (firstColumn > 0)
            --firstColumn;

        var last = shipSquares.Last();
        var lastRow = last.Row;
        var lastColumn = last.Column;

        if (lastRow < rows - 1)
            ++lastRow;
        if (lastColumn < columns - 1)
            ++lastColumn;

        var result = new List<SquareCoordinate>();
        for (var r = firstRow; r <= lastRow; ++r)
        for (var c = firstColumn; c <= lastColumn; ++c)
            result.Add(new SquareCoordinate(r, c));
        return result;
    }
}