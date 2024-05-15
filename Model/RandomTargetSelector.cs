namespace Vsite.Oom.Battleship.Model;

public class RandomTargetSelector : ITargetSelector
{
    private readonly Grid grid;
    private readonly int shipLength;
    private readonly Random random = new Random();
    public RandomTargetSelector(Grid grid, int shipLength)
    {
        this.grid = grid;
        this.shipLength = shipLength;
    }

    public Square Next()
    {
        var placements = grid.GetAvailablePlacements(shipLength);
        var candidates = placements.SelectMany(s => s); //1D polje iz 2D
        var selectedIndex = random.Next(candidates.Count());

        return candidates.ElementAt(selectedIndex);
    }
}