using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var mostCommonSquares = largestGroups.Select(g => g.Key);

            if (mostCommonSquares.Count() == 1)
            {
                mostCommonSquares.First();
            }

            int index = random.Next(mostCommonSquares.Count());

            return mostCommonSquares.ElementAt(index);
            throw new NotImplementedException();
        }
    }
}
