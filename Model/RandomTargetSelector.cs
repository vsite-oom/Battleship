namespace Vsite.Oom.Battleship.Model;

public class RandomTargetSelector(ShotsGrid fleetGrid, int shipLength) : ITargetSelector
{
    private readonly Random random = new Random();

    public Square Next()
    {
        var placements = fleetGrid.GetAvailablePlacements(shipLength);
        var candidates = placements.SelectMany(s => s); //1D polje iz 2D
        var selectedIndex = random.Next(candidates.Count());

        return candidates.ElementAt(selectedIndex);
    }
}