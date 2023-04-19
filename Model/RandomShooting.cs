using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Vsie.Oom.Battleship.Model
{
    public class RandomShooting: IShootingTactics
    {
        public RandomShooting(Grid grid)
        {
            this.grid = grid;
        }

        private readonly Grid grid;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
