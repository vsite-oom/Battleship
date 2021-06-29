using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ISelectTarget
    {
        public RandomShooting(Grid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
        }

        public Square NextTarget()
        {
            // get list of all possible placements for a ship of given length
            var sequences = grid.GetSequences(shipLength);
            // create a simple array with all squares (some of them will appear multiple times)
            var allCandidates = sequences.SelectMany(seq => seq);
            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);
            // find the number of squares in the largests group
            var maxCount = groups.Max(g => g.Count());
            // filter out only groups which contain maxCount squares
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            // fetch keys from each group (i.e. square that represents the group)
            var mostOften = largestGroups.Select(g => g.Key);
            if (mostOften.Count() == 1)
                return mostOften.First();
            int index = random.Next(mostOften.Count());
            return mostOften.ElementAt(index);
        }

        private Grid grid;
        private int shipLength;
        private Random random = new Random();
    }
}
