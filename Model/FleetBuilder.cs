using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class FleetBuilder
    {
        private readonly Grid fleetGrid;
        private readonly List<int> shipLengths;
        public FleetBuilder(int gridRows,int gridColumns, int[] shipLength) {
            fleetGrid = new Grid(gridRows, gridColumns);
            shipLengths = new List<int>(shipLength);
        }

        public Fleet CreateFleet()
        {
            var fleet=new Fleet();
            for(int i = 0; i < shipLengths.Count; ++i)
            {
                var candidates = fleetGrid.GetAvailablePlacements(shipLengths[i]);
                var selectedIndex = random.Next(candidates.Count());
                var selected = candidates.ElementAt(selectedIndex);
                fleet.CreateShip(selected);
            }
            throw new NotImplementedException();
        }
    }
}
