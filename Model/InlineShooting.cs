using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineShooting : ITargetSelect
    {
        public InlineShooting(Grid evidenceGrid, SortedSquares squaresHit)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
        }
        public Square NextTarget(int shipLength)
        {
            var l = evidenceGrid.GetSquaresInline(squaresHit);

            if (l.Count() == 1)
                return l.ElementAt(0).First();
            var order = l.OrderByDescending(ls => ls.Count());
            int maxLen = order.First().Count();
            if (maxLen > shipLength - squaresHit.Length)
                maxLen = shipLength - squaresHit.Length;
            var longest = order.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());

            return longest.ElementAt(index).First();
        }

        private Random random = new Random();
        private readonly Grid evidenceGrid;
        private readonly SortedSquares squaresHit;
    }
}
