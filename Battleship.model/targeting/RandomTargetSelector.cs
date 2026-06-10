namespace Battleship.Model;

/// <summary>Selects a random available target square from positions where a ship of the configured length could fit.</summary>
public class RandomTargetSelector : ITargetSelector
{
    private readonly ShotsGrid grid;
    private readonly int shipLength;
    private readonly Random random = new Random();

    /// <summary>Initializes a new instance of the <see cref="RandomTargetSelector"/> class.</summary>
    /// <param name="grid">The shots grid used to find available placements.</param>
    /// <param name="shipLength">The minimum ship length used to filter candidate squares.</param>
    public RandomTargetSelector(ShotsGrid grid, int shipLength)
    {
        this.grid = grid;
        this.shipLength = shipLength;
    }

    /// <inheritdoc/>
    public Square Next()
    {
        var placements = grid.GetAvailablePlacements(shipLength);
        var candidates = placements.SelectMany(s => s).ToArray();
        var selectedIndex = random.Next(candidates.Length);
        return candidates[selectedIndex];
    }
}
