using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        private readonly Grid grid;
        private List<Square> squares;
        private readonly IEnumerable<int> shipLengths;
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
            this.shipLengths = shipLengths;
        }
        public Square AddNextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
