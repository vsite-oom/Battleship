using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelect
    {
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private readonly Random random = new Random();

        public RandomShooting(Grid grid, List<int> shipsToShoot)
        {
            evidenceGrid = grid;
            this.shipsToShoot = shipsToShoot;
        }

        public Square NextTarget()
        {
            int shipLength = shipsToShoot[0];
            var placements = evidenceGrid.GetAvailablePlacements(shipLength);
            // create simple array of sqaures from arrays of arrays
            var allCandidates = placements.SelectMany(seq => seq);
            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);
            // find the number of squares in largest group
            var maxCount = groups.Max(g => g.Count());
            // filter only froups that have maxCount elements
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            // fetch keys from each group (i.e. square that represents the group)
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count() == 1)
                return mostCommon.First();
            var index = random.Next(0, mostCommon.Count());
            return mostCommon.ElementAt(index);
        }
    }
}
