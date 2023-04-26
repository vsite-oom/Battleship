using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLenghts)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
            this.shipLenghts = shipLenghts; 

        }

        private readonly Grid grid;
        private List<Square> squares;
        private readonly IEnumerable<int> shipLenghts; 
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
