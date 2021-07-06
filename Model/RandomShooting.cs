using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelect
    {
        public RandomShooting(Grid grid, List<int> shipsToShoot)
        {
            this.grid = grid;
            this.shipsToShoot = shipsToShoot;
            random = new Random();
        }

        public Square NextTarget()
        {
            var allPlacements = grid.GetAvailablePlacements(shipsToShoot[0]);
            Debug.Assert(allPlacements.Count() != 0);
            // create simple array of all squares
            var allCandidates = allPlacements.SelectMany(seq => seq);

            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);

            // find number of squares in the largest group
            int maxCount = groups.Max(g => g.Count());

            // filter groups with count == maxCount
            var largestGroups = groups.Where(g => g.Count() == maxCount);

            // fetch keys from highestOccurrence
            var mostCommonSquares = largestGroups.Select(g => g.Key);

            if (mostCommonSquares.Count() == 1)
            {
                mostCommonSquares.First();
            }

            int index = random.Next(mostCommonSquares.Count());

            return mostCommonSquares.ElementAt(index);
        }

        private Grid grid;
        private List<int> shipsToShoot;

        Random random;
    }
}