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
        private readonly IEnumerable<int> shiplenghts;
        private Random random = new Random();
        public RandomShooting(Grid grid, IEnumerable<int> shipLenghts)
        {
            this.grid = grid;
            this.shiplenghts = shipLenghts;
        }
        public Square NextTarget()
        {
            var shiplenght = shiplenghts.Max();
            var avaliableSequences = grid.GetAvaliableSequences(shiplenght);
            var candidates = avaliableSequences.SelectMany(s => s);
            var index=random.Next(candidates.Count());
            return candidates.ElementAt(index);
        }
    }
}
