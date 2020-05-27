using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundShooting : ITargetSelect
    {
        public SurroundShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
        }

        public Square NextTarget()
        {
            List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);

                if (l.Count() > 0)
                    arround.Add(l);
            }

            if (arround.Count == 1)
                return arround[0].First();
            var order = arround.OrderByDescending(ls => ls.Count());
            int maxLen = order.First().Count();
            int shipLength = shipsToShoot[0];
            if (maxLen > shipLength - 1)
                maxLen = shipLength - 1;
            var longest = order.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());

            return longest.ElementAt(index).First();
        }

        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        private readonly List<int> shipsToShoot;
    }
}
