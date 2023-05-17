using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vsite.Oom.Battleship.Model.Grid;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : IShootingTactics
    {
        public RandomShooting(RecordGrid grid, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.shipLengths = shipLengths;
        }

        private readonly RecordGrid grid;
        private readonly IEnumerable<int> shipLengths;
        private Random random = new Random();

        public Square NextTarget()
        {
            var shipLength = shipLengths.Max();
            var availableSequences = grid.GetAvailableSequences(shipLength);
            var candidates = availableSequences.SelectMany(s => s);
            int index = random.Next(candidates.Count());
            return candidates.ElementAt(index);
        }
    }
}