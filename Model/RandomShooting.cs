using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelector
    {
        private Random random = new Random();
        private readonly Grid evidenceGrid;

        public RandomShooting(Grid grid)
        {
            evidenceGrid = grid;
        }
        public Square NextTarget(int shipLength)
        {
            //var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]).SelectMany(s => s);
            //var index = random.Next(0, placements.Count());
            //return placements.ElementAt(index);
            return null;
        }


    }
}
