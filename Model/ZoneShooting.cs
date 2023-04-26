using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    internal class ZoneShooting
    {
        public ZoneShooting(Grid grid, Square firstHit)
        {
            this.grid = grid;
            this.firstHit = firstHit;
        }

        private readonly Grid grid;
        private readonly Square firstHit;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
