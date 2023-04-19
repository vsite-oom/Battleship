using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Vsie.Oom.Battleship.Model
{
    public class ZoneShooting: IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstSquareHit)
        {
            this.grid = grid;
            firstHit = firstSquareHit;
        }

        private readonly Grid grid;
        private readonly Square firstSquareHit;
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
