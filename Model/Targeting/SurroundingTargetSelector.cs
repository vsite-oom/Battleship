namespace Model;

public class SurroundingTargetSelector : ITargetSelector
{
    private readonly ShotsGrid _grid;
    private readonly Square _hitSquare;
    private readonly Random _random = new();

    public SurroundingTargetSelector(ShotsGrid grid, Square hitSquare)
    {
        _grid = grid;
        _hitSquare = hitSquare;
    }

    public Square Next()
    {
        var candidates = new List<IEnumerable<Square>>();

        foreach (Direction direction in Enum.GetValues<Direction>())
        {
            var inDirection = _grid.GetSquaresInDirection(
                _hitSquare.Position.Row, _hitSquare.Position.Column, direction);
            if (inDirection.Any())
            {
                candidates.Add(inDirection);
            }
        }

        if (candidates.Count == 0)
        {
            var fallback = _grid.Squares.FirstOrDefault(s => s.State == SquareState.None);
            if (fallback != null) return fallback;
            throw new InvalidOperationException("No targets available.");
        }

        int index = _random.Next(candidates.Count);
        return candidates[index].First();
    }
}
