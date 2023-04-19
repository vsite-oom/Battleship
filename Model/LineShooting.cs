using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(Grid grid, IEnumerable<Square> squaresHit)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
        }

        private readonly Grid grid;
        private List<Square> squares;
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
