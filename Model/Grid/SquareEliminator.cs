namespace Model;

public class SquareEliminator
{
    public IEnumerable<Coordinate> ToEliminate(IEnumerable<Square> shipSquares, int gridRows, int gridColumns)
    {
        var result = new HashSet<Coordinate>();
        foreach (var square in shipSquares)
        {
            int r = square.Position.Row;
            int c = square.Position.Column;
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    int nr = r + dr;
                    int nc = c + dc;
                    if (nr >= 0 && nr < gridRows && nc >= 0 && nc < gridColumns)
                    {
                        result.Add(new Coordinate(nr, nc));
                    }
                }
            }
        }
        return result;
    }
}
