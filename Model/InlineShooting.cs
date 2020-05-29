using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineShooting : ITargetSelect
    {
        private readonly SortedSquares squaresHit;
        private Random random = new Random();
        private Grid evidenceGrid;
        private List<int> shipsToShoot;

        public InlineShooting(Grid grid, SortedSquares squares, List<int> shipsToShoot)
        {
            squaresHit = squares;
            evidenceGrid = grid;
            this.shipsToShoot = shipsToShoot;
        }

        public Square NextTarget()
        {
            var shipLength = shipsToShoot[0];
            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();
            var ordered = l.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipLength - squaresHit.Count())
                maxLen = shipLength - squaresHit.Count();
            var longest = ordered.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());
            return longest.ElementAt(index).First();
        }
    }
}
