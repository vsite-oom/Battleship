using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineShooting : ITargetSelect
    {
        public InlineShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.evidenceGrid = evidenceGrid;
            this.squaresHit = squaresHit;
            this.shipsToShoot = shipsToShoot;
        }
        public Square NextTarget()
        {
            int shipLength = shipsToShoot[0];

            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            //TODO: inprove selection so that only largest list are taken as candidates.
            l.OrderByDescending(ls => ls.Count());
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }
        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        private readonly List<int> shipsToShoot;

    }

}
