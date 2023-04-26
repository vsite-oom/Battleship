using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private readonly Square firstHit;
        private readonly IEnumerable<int> shipLengths;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
