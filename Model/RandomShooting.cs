using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model {
    public class RandomShooting : ITargetSelect {
        private readonly Grid Grid;
        private readonly List<int> ShipsToShoot;
        private readonly Random random = new Random();

        public RandomShooting(Grid grid, List<int> shipsToShoot) {
            Grid = grid;
            ShipsToShoot = shipsToShoot;
        }

        public Square NextTarget() {
            var allPlacements = Grid.GetAvailablePlacements(ShipsToShoot[0]);

            var allCandidates = allPlacements.SelectMany(seq => seq);

            var groups = allCandidates.GroupBy(sq => sq);

            var maxCount = groups.Max(g => g.Count());

            var largestGroups = groups.Where(g => g.Count() == maxCount);

            var mostCommunSquares = largestGroups.Select(g => g.Key);

            if (mostCommunSquares.Count() == 1) {
                mostCommunSquares.First();
            }

            int index = random.Next(mostCommunSquares.Count());

            return mostCommunSquares.ElementAt(index);
        }
    }
}