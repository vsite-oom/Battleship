namespace Model;

public class RandomTargetSelector : ITargetSelector
{
    private readonly ShotsGrid _grid;
    private readonly int _shipLength;
    private readonly Random _random = new();

    public RandomTargetSelector(ShotsGrid grid, int shipLength)
    {
        _grid = grid;
        _shipLength = shipLength;
    }

    public Square Next()
    {
        var placements = _grid.GetAvailablePlacements(_shipLength);
        var candidates = placements.SelectMany(s => s).Distinct().ToList();

        if (candidates.Count == 0)
        {
            var fallback = _grid.Squares.Where(s => s.State == SquareState.None).ToList();
            if (fallback.Count > 0)
                return fallback[_random.Next(fallback.Count)];
            throw new InvalidOperationException("No targets available.");
        }

        return candidates[_random.Next(candidates.Count)];
    }
}
