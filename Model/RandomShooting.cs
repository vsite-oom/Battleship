using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
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
            var groups = allCandidates.GroupBy(sq => sq);
            var maxCount = groups.Max(g => g.Count());
            var largestGroup = groups.Where(g => g.Count() == maxCount);
            var mostCommon = largestGroup.Select(g => g.Key);

            if (mostCommon.Count() == 1)
                return mostCommon.First();
            int index = random.Next(0, mostCommon.Count());

            return mostCommon.ElementAt(index);
        }

        private Random random = new Random();
        private readonly Grid evidenceGrid;
    }
}
