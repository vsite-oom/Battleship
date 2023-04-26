using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.squaresHit = new List<Square>(squaresHit);
            this.shipLengths = shipLengths;
        }

        private readonly Grid grid;
        private List<Square> squaresHit;
        private readonly IEnumerable<int> shipLengths;

        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
