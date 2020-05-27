using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class SurroundingShooting : ITargetSelect
    {
        public SurroundingShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
        }
        public Square NextTarget()
        {
            int shipLength = shipsToShoot[0];
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                {
                    around.Add(l);
                }
            }

            if (around.Count() == 1)
            {
                return around[0].First();
            }
            //TODO: improve selection so that only largest lists are taken as candidates
            int index = random.Next(0, around.Count);
            return around[index].First();
        }

        private readonly List<int> shipsToShoot;
        private readonly Grid evidenceGrid;
        private readonly SortedSquares squaresHit;
        private Random random = new Random();
    }
}
