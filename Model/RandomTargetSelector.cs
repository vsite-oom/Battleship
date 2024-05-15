namespace Vsite.Oom.Battleship.Model;

public class RandomTargetSelector : ITargetSelector
{
    private readonly FleetGrid _fleetGrid;
    private readonly int shipLength;
    private readonly Random random = new Random();
    public RandomTargetSelector(FleetGrid fleetGrid, int shipLength)
    {
        this._fleetGrid = fleetGrid;
        this.shipLength = shipLength;
    }

    public Square Next()
    {
        var placements = _fleetGrid.GetAvailablePlacements(shipLength);
        var candidates = placements.SelectMany(s => s); //1D polje iz 2D
        var selectedIndex = random.Next(candidates.Count());

        return candidates.ElementAt(selectedIndex);
    }
}