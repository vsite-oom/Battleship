using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : IShootingTactics
    {
        public RandomShooting(Grid grid, IEnumerable<int> shipLenghts) 
        { 
            this.grid = grid;
            this.shipLenghts = shipLenghts;
        }
        private readonly Grid grid;
        private readonly IEnumerable<int> shipLenghts;
        private Random random = new Random();
        public Square NextTarget()
        {
            var shipLenght = shipLenghts.Max();
            var availableSequences = grid.GetAvailableSquences(shipLenght);
            var candidates = availableSequences.SelectMany(s => s);
            int index = random.Next(candidates.Count());
            return candidates.ElementAt(index);
        }

    }
}
