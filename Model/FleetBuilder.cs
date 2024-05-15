namespace Vsite.Oom.Battleship.Model;

public class FleetBuilder
{
    private readonly SquareEliminator eliminator = new();
    //FirstPart -> Initialization of game; 

    private readonly FleetGrid? fleetGrid;
    private readonly Random random = new();

    private readonly List<int> shipLengths;

    public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
    {
        fleetGrid = new FleetGrid(gridRows, gridColumns);
        this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
    }

    public Fleet CreateFleet()
    {
        var fleet = new Fleet();

        try
        {
            foreach (var shipPositions in shipLengths)
            {
                var candidates = fleetGrid?.GetAvailablePlacements(shipPositions);
                var selectedIndex = random.Next(candidates!.Count());
                if (candidates == null)
                    continue;
                var selected = candidates.ElementAt(selectedIndex);
                fleet.CreateShip(selected);

                var toEliminate = eliminator.ToEliminate(selected, fleetGrid!.Rows, fleetGrid!.Columns);

                foreach (var coordinate in toEliminate) fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
            }
        }
        catch (NullReferenceException)
        {
            var noviGrid = new FleetGrid(fleetGrid!.Rows, fleetGrid!.Columns);
            return CreateFleet();
        }

        return fleet;
    }
}