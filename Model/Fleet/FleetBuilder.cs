namespace Model;

public class FleetBuilder
{
    private readonly FleetGrid _grid;
    private readonly List<int> _shipLengths;
    private readonly Random _random = new();
    private readonly SquareEliminator _eliminator = new();

    public FleetBuilder(int rows, int columns, int[] shipLengths)
    {
        ArgumentNullException.ThrowIfNull(shipLengths);
        _grid = new FleetGrid(rows, columns);
        _shipLengths = new List<int>(shipLengths.OrderByDescending(l => l));
    }

    public Fleet CreateFleet()
    {
        var fleet = new Fleet();

        foreach (var length in _shipLengths)
        {
            var candidates = _grid.GetAvailablePlacements(length).ToArray();
            var index = _random.Next(candidates.Length);
            var selected = candidates[index];

            fleet.CreateShip(selected);

            var toEliminate = _eliminator.ToEliminate(selected, _grid.Rows, _grid.Columns);
            foreach (var coord in toEliminate)
            {
                _grid.EliminateSquare(coord.Row, coord.Column);
            }
        }

        return fleet;
    }
}
