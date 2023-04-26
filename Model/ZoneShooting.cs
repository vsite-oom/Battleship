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
        private readonly IEnumerable<int> shiplenghts;
        public ZoneShooting(Grid grid,Square firstHit, IEnumerable<int> shipLenghts)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shiplenghts = shipLenghts;
        }
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
