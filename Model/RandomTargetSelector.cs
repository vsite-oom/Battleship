using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class RandomTargetSelector : ITargetSelector
    {
        private readonly FleetGrid grid;
        private readonly int shipLength;
        private readonly Random random=new Random();
        public RandomTargetSelector(FleetGrid grid, int shipLength) {
            this.grid = grid;
            this.shipLength = shipLength;
        }
        public Square Next()
        {
            var placements = grid.GetAvailablePlacements(shipLength);
            var candidates=placements.SelectMany(s => s);
            var selectedIndex = random.Next(candidates.Count());
            return candidates.ElementAt(selectedIndex);
        }
    }
}
