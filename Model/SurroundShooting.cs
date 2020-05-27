using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundShooting : ITargetSelect
    {
        public SurroundShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.evidenceGrid = evidenceGrid;
            this.squaresHit = squaresHit;
            this.shipsToShoot = shipsToShoot;
        }
        public Square NextTarget()
        {
            int shipLength = shipsToShoot[0];
            List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                    arround.Add(l);
            }
            if (arround.Count == 1)
                return arround[0].First();
            //TODO: inprove selection so that only largest list are taken as candidates.
            int index = random.Next(0, arround.Count());
            return arround[index].First();
        }
        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        private readonly List<int> shipsToShoot;



    }
}
