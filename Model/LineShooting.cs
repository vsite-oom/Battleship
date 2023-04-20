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
        private readonly List<Square> squaresHit;
        public LineShooting(Grid grid,IEnumerable<Square> squaresHit)
        {
            
            this.grid = grid;
            this.squaresHit=new List<Square>(squaresHit);
        }
        public Square NextTarget()
        {
            throw new NotImplementedException();
        }
    }
}
