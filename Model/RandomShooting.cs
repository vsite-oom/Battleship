using System;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelect
    {
        public RandomShooting(Grid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
        }

        public Square NextTarget()
        {
            // vraća nizove nizova
            var allPlacements = grid.GetAvailablePlacements(shipLength);
            // create simple array of all squares
            var allCandidates= allPlacements.SelectMany(seq => seq);
            // create geoups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);
            // find naumber of squares in the largest
            var maxCount = groups.Max(g => g.Count());
            // filter groups with count == maxCount
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            // fetch keys from largestGroups
            var mostComomnSquares = largestGroups.Select(g => g.Key);
            if (mostComomnSquares.Count() == 1)
                mostComomnSquares.First();
            int index = random.Next(mostComomnSquares.Count());
            return mostComomnSquares.ElementAt(index);
        }

        private Grid grid;
        private int shipLength;
        private Random random = new Random();
    }
}
