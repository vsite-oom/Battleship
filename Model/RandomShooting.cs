using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : IShootingTactics
    {
        private readonly Grid grid;
        private readonly IEnumerable<int> shipLengths;
        private Random random = new();
        public RandomShooting(Grid grid, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.shipLengths = shipLengths;
        }
        public Square AddNextTarget()
        {
            var shipLength = shipLengths.Max();
            var availableSequences = grid.GetAvailableSequences(shipLength);
            var candidates = availableSequences.SelectMany(s => s);

            return candidates.ElementAt(random.Next(candidates.Count()));
        }
    }
}
