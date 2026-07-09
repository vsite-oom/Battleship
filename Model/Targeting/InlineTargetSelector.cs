namespace Model;

public class InlineTargetSelector : ITargetSelector
{
    private readonly ShotsGrid _grid;
    private readonly List<Square> _hitSquares;

    public InlineTargetSelector(ShotsGrid grid, List<Square> hitSquares)
    {
        _grid = grid;
        _hitSquares = hitSquares.OrderBy(s => s.Position.Row).ThenBy(s => s.Position.Column).ToList();
    }

    public Square Next()
    {
        var first = _hitSquares.First();
        var last = _hitSquares.Last();

        bool isHorizontal = first.Position.Row == last.Position.Row;

        if (isHorizontal)
        {
            for (int c = last.Position.Column + 1; c < _grid.Columns; c++)
            {
                var sq = GetSquareAt(first.Position.Row, c);
                if (sq != null && sq.State == SquareState.None) return sq;
                if (sq == null || sq.State == SquareState.Missed || sq.State == SquareState.Eliminated) break;
            }
            for (int c = first.Position.Column - 1; c >= 0; c--)
            {
                var sq = GetSquareAt(first.Position.Row, c);
                if (sq != null && sq.State == SquareState.None) return sq;
                if (sq == null || sq.State == SquareState.Missed || sq.State == SquareState.Eliminated) break;
            }
        }
        else
        {
            for (int r = last.Position.Row + 1; r < _grid.Rows; r++)
            {
                var sq = GetSquareAt(r, first.Position.Column);
                if (sq != null && sq.State == SquareState.None) return sq;
                if (sq == null || sq.State == SquareState.Missed || sq.State == SquareState.Eliminated) break;
            }
            for (int r = first.Position.Row - 1; r >= 0; r--)
            {
                var sq = GetSquareAt(r, first.Position.Column);
                if (sq != null && sq.State == SquareState.None) return sq;
                if (sq == null || sq.State == SquareState.Missed || sq.State == SquareState.Eliminated) break;
            }
        }

        foreach (var hit in _hitSquares)
        {
            int r = hit.Position.Row;
            int c = hit.Position.Column;
            int[][] deltas = isHorizontal
                ? new[] { new[] { -1, 0 }, new[] { 1, 0 } }
                : new[] { new[] { 0, -1 }, new[] { 0, 1 } };

            foreach (var d in deltas)
            {
                var sq = GetSquareAt(r + d[0], c + d[1]);
                if (sq != null && sq.State == SquareState.None) return sq;
            }
        }

        var fallback = _grid.Squares.FirstOrDefault(s => s.State == SquareState.None);
        if (fallback != null) return fallback;

        throw new InvalidOperationException("No targets available.");
    }

    private Square? GetSquareAt(int row, int column)
    {
        if (row < 0 || row >= _grid.Rows || column < 0 || column >= _grid.Columns)
            return null;

        return _grid.Squares.FirstOrDefault(s =>
            s.Position.Row == row && s.Position.Column == column);
    }
}
