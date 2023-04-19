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
        public RandomShooting(Grid grid)
        {
            this.grid = grid;
        }
        public Square AddNextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
