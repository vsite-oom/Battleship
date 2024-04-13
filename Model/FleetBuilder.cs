using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shiplengths)
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shiplengths = new List<int>(shiplengths.OrderByDescending(length => length));
        }

        private readonly Grid fleetGrid;

        private readonly List<int> shiplengths;

        public Fleet CreateFleet()
        {
            var fleet = new Fleet();
            for (int i = 0; i < shiplengths.Count; i++)
            {
                var candidates = fleetGrid.GetVerticalAvailablePlacements(shiplengths[i]);
                var selectedIndex = random.Next(candidates.Count());
                var selected = candidates.ElementAt(selectedIndex);

                fleet.CreateShip(selected);


            }

            return fleet;
        }

        private readonly Random random;
       
    }
}
