using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelect
    {
        private readonly Grid Grid;
        private readonly int ShipLenght;
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private Random random = new Random();

        public RandomShooting(Grid grid, int shipLenght)
        {
            Grid = grid;
            ShipLenght = shipLenght;
        }

        public RandomShooting(Grid evidenceGrid, List<int> shipsToShoot)
        {
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
        }

        public Square NextTarget()
        {
            var allPlacements = Grid.GetAvailablePlacements(ShipLenght);

            // create simple array of all squares
            var allCandidates = allPlacements.SelectMany(seq => seq);

            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);

            // find number of squares in the largest group
            var maxCount = groups.Max(g => g.Count());

            // filter groups with count == maxCount
            var largestGroups = groups.Where(g => g.Count() == maxCount);

            // fetch keys from largestGroups
            var mostCommunSquares = largestGroups.Select(g => g.Key);

            if (mostCommunSquares.Count() == 1)
            {
                mostCommunSquares.First();
            }

            int index = random.Next(mostCommunSquares.Count());
            return mostCommunSquares.ElementAt(index);

            throw new NotImplementedException();
        }
    }
}
