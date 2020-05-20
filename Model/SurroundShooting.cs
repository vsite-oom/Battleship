using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vsite.Oom.Battleship.Model.Grid;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundShooting : ITargetSelect
    {
        public SurroundShooting(Grid evidenceGrid,SortedSquares squaresHit)
        {
            this.evidenceGrid = evidenceGrid;
            this.squaresHit = squaresHit;
        }
        public Square NextTarget(int shipLength)
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                    around.Add(l);
            }

            if (around.Count == 1)
                return around[0].First();
            //TODO: improve selection so that only largest lists are taken into account
            var ordered = around.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipLength- 1)
                maxLen = shipLength - 1;
            var longest = ordered.Where(ls => ls.Count() >= maxLen);

            int index = random.Next(0, around.Count);
            return around[index].First();
        }
        private Random random = new Random();
        private readonly Grid evidenceGrid;

        private readonly SortedSquares squaresHit;
    }
}
