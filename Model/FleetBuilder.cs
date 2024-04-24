namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        //FirstPart -> Initialization of game; 

        private Grid fleetGrid;
        private readonly List<int> shipLengths;
        private readonly Random random = new Random();
        private readonly SquareEliminator eliminator = new SquareEliminator();

        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
        {
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
        }

        public Fleet CreateFleet()
        {
            var fleet = new Fleet();

            fleetGrid = new Grid(fleetGrid.Rows, fleetGrid.Columns);

            try
            {
                foreach (var shipPositions in shipLengths)
                {
                    var candidates = fleetGrid.GetAvailablePlacements((shipPositions));
                    var selectedIndex = random!.Next(candidates.Count());
                    var selected = candidates.ElementAt(selectedIndex);
                    fleet.CreateShip(selected);

                    var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);

                    foreach (var coordinate in toEliminate)
                    {
                        fleetGrid.EleminateSquare(coordinate.Row, coordinate.Column);
                    }

                }
            }
            catch (Exception)
            {
                return CreateFleet();
            }

            return fleet;
        }
    }
}
