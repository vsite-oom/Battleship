using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{    
    public class SurroundShooting : ITargetSelector
    {
        private Random random = new Random();
        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;

        public SurroundShooting(Grid grid, SortedSquares squaresH)
        {
            squaresHit = squaresH;
            evidenceGrid = grid;
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
            if (around.Count() == 1)
                return around[0].First();

            //around.OrderByDescending(arounnd => around.Count());
            //TODO: improve list selection to take only largest list
            var index = random.Next(0, around.Count());

            return around[index].First();
        }
    }
}
