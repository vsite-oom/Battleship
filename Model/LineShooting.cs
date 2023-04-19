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
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
        }
        public Square AddNextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
