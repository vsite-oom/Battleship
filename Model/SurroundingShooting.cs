using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingShooting : ISelectTarget
    {
        public SurroundingShooting(Grid grid, Square initiallyHit, int shipLength)
        {
            this.grid = grid;
            this.initiallyHit = initiallyHit;
            this.shipLength = shipLength;
        }
        public Square NextTarget()
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            var squares = grid.GetSequence(initiallyHit, Direction.Up);
            if (squares.Count() > 0)
                result.Add(squares);
            squares = grid.GetSequence(initiallyHit, Direction.Right);
            if (squares.Count() > 0)
                result.Add(squares);
            squares = grid.GetSequence(initiallyHit, Direction.Down);
            if (squares.Count() > 0)
                result.Add(squares);
            squares = grid.GetSequence(initiallyHit, Direction.Left);
            if (squares.Count() > 0)
                result.Add(squares);
            // if squares are only in one direction
            if (result.Count() == 1)
                return result[0].First();
            // sort list from longest to shortest
            var ordered = result.OrderByDescending(seq => seq.Count());
            int maxLen = result.First().Count();
            if (maxLen > shipLength - 1)
                maxLen = shipLength - 1;
            // filter out all arrays for which length is equal or larger than shipLength - 1
            var longestCandidates = ordered.Where(seq => seq.Count() >= maxLen);
            int index = random.Next(longestCandidates.Count());
            return longestCandidates.ElementAt(index).First();
        }

        private Grid grid;
        private Square initiallyHit;
        private int shipLength;
        private Random random = new Random();
    }
}
