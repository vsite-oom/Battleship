using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingShooting : ITargetSelect
    {
        public SurroundingShooting(Grid grid, Square firstHit, int shipLength)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLength = shipLength;
        }
        public Square NextTarget()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            
            var up = grid.GetAvailablePlacementsInDirection(firstHit, Direction.Upwards);
            if (up.Count() > 0)
                squares.Add(up);
            var right = grid.GetAvailablePlacementsInDirection(firstHit, Direction.Rightwards);
            if (right.Count() > 0)
                squares.Add(right);
            var down = grid.GetAvailablePlacementsInDirection(firstHit, Direction.Downwards);
            if (down.Count() > 0)
                squares.Add(down);
            var left = grid.GetAvailablePlacementsInDirection(firstHit, Direction.Leftwards);
            if (left.Count() > 0)
                squares.Add(left);

            // sort squares by length
            var sorted = squares.OrderByDescending(seq=>seq.Count());
            int maxLength = sorted.ElementAt(0).Count();
            if (maxLength > shipLength - 1)
                maxLength = shipLength - 1;
            var longest = sorted.Where(seq => seq.Count() >= maxLength);
            if (longest.Count() == 1)
                return longest.ElementAt(0).First();
            int index = random.Next(longest.Count());
            return longest.ElementAt(index).First();
        }

        private Grid grid;
        private Square firstHit;
        private readonly int shipLength;
        private Random random = new Random();
    }
}
