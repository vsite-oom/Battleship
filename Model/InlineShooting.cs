using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineShooting : ITargetSelect
    {
        public InlineShooting(SortedSquares squaresHit, Grid evidenceGrid, List<int> shipsToShoot)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
        }

        public Square NextTarget()
        {
            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            var ordered = l.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            int shipLength = shipsToShoot[0];
            if (maxLen > shipLength - 1)
                maxLen = shipLength - 1;
            var longest = ordered.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());
            return longest.ElementAt(index).First();

            //l = l.OrderByDescending(s => s.Count());

            //return l.First().First();
        }

        Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        private readonly List<int> shipsToShoot;

    }
}
