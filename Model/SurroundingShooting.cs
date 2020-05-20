using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingShooting : ITargetSelect
    {
        public SurroundingShooting(Grid evidenceGrid,SortedSquares squaresHit)
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
            if (arround.Count == 1)
            {
                return arround[0].First();
            }
            //improve  selection so that only largest lists are taken as candidates
            int index = random.Next(0, arround.Count);
            return arround[index].First();
        }
        private Random random = new Random();
        private readonly Grid evidenceGrid;
        private readonly SortedSquares squaresHit;
    }

}
