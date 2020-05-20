using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundShooting : ITargetSelect
    {
        public SurroundShooting(SortedSquares squaresHit, Grid evidenceGrid)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
        }
        public Square NextTarget(int shipLength)
        {
            List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);

                if (l.Count() > 0)
                    arround.Add(l);
            }

            if (arround.Count() == 1)
                return arround[0].First();


            var ordered = arround.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipLength - 1)
                maxLen = shipLength - 1;
            var longest = ordered.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());
            return longest.ElementAt(index).First();
           
            
            //shipsToShoot.Sort((a, b) => a.CompareTo(b));
            //int longestShip = shipsToShoot.FirstOrDefault();
            //arround = arround.OrderByDescending(s => s.Count()).ToList();
            //arround.RemoveAll(s => s.Count() < longestShip - squaresHit.Length);

            //return arround[random.Next(0,arround.Count)].First();
        }

        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
    }
}
