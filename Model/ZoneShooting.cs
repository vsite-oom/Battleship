using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(Grid grid, Square firstSquareHit, IEnumerable<int> shipLenghts)
        {
            this.grid = grid;
            this.firstHit = firstSquareHit;
            this.shipLenghts = shipLenghts;
        }
        private readonly Grid grid;
        private readonly Square firstHit; 
        private readonly IEnumerable<int> shipLenghts;
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
