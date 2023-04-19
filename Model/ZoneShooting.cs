using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        private readonly Grid grid;
        private readonly Square firstHit;

        public ZoneShooting(Grid grid, Square firstHit)
        {
            this.grid = grid;
            this.firstHit = firstHit;
        }
        public Square AddNextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
