using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineShooting : ITargetSelect
    {
        public InlineShooting(Grid evidenceGrid,SortedSquares squaresHit) {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
        }
        public Square NextTarget(int shipLenght)
        {
            var l = evidenceGrid.GetSquaresInLine(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            l.OrderByDescending(ls => ls.Count());
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }
        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
    }
}
