using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class RandomShooting : ITargetSelect
    {
        public RandomShooting(Grid evidenceGrid)
        {
            this.evidenceGrid = evidenceGrid;
        }

        public Square NextTarget(int shipLength)
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipLength);
            var allCandidates = placements.SelectMany(seq => seq);
            int index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }

        private readonly Grid evidenceGrid;
        private Random random = new Random();
    }
}
