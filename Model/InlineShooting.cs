using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class InlineShooting : ITargetSelect
    {
        public InlineShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
    }

        public Square NextTarget()
        {
            int shipLength = shipsToShoot[0];
            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
            {
                return l.ElementAt(0).First();
            }
            //TODO: improve selection so that only largest lists are taken as candidates
            l.OrderByDescending(ls => ls.Count());
            int index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }

        private readonly List<int> shipsToShoot;
        private readonly Grid evidenceGrid;
        private Random random = new Random();
        private readonly SortedSquares squaresHit;
    }
}
