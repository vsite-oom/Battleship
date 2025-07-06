using Model;
using System;
using System.Linq;

public class RandomTargetSelector : ITargetSelector
{
    public RandomTargetSelector(ShotsGrid grid, int shipLength)
    {
        this.grid = grid;
        this.shipLength = shipLength;
    }

    public Square Next()
    {
        var placements = grid.GetAvailablePlacements(shipLength);
        var candidates = placements.SelectMany(s => s).ToList();

        if (!candidates.Any())
            throw new InvalidOperationException("No available placements for the given ship length.");

        var selectedIndex = random.Next(candidates.Count);
        return candidates[selectedIndex];
    }

    private readonly ShotsGrid grid;
    private readonly int shipLength;
    private readonly Random random = new Random();
}
