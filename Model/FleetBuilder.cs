namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths) 
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shipLengths = new List<int> (shipLengths);
        }

        private readonly Grid fleetGrid;

        private readonly List<int> shipLengths;
    }

}
