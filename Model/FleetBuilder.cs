using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
        }

        private readonly Grid fleetGrid;

        private readonly List<int> shipLengths;

        public Fleet CreateFleet()
        {
            var fleet = new Fleet();

            for (int i = 0; i < shipLengths.Count; ++i)
            {
                var candidates = fleetGrid.GetAvailablePlacements(shipLengths[i]);
                var selectedIndex = random.Next(candidates.Count());
                var selected = candidates.ElementAt(selectedIndex);

                fleet.CreateShip(selected);

                var toEliminate=eliminator.ToEliminate(selected,fleetGrid.Rows, fleetGrid.Columns);
                foreach(var coords in toEliminate)
                {
                    fleetGrid.EliminateSquare(coords.row, coords.col);
                }

            }

            return fleet;
        }

        private readonly Random random = new Random();
        private readonly SquareEliminator eliminator=new SquareEliminator();
    }
}
