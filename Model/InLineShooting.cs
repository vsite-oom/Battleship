using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InLineShooting : ITargetSelector
    {
        private Random random = new Random();

        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        public InLineShooting(Grid grid, SortedSquares squaresHit)
        {
            this.squaresHit = squaresHit;
            evidenceGrid = grid;
        }

        public Square NextTarget(int shipLength)
        {
            var l = evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            var index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }
        
    }
}
