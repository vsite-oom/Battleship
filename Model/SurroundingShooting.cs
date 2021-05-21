using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingShooting : ITargetSelect
    {
        public SurroundingShooting(Grid grid, Square FirstHit, int shipLength)
        {
            this.grid = grid;
            this.firstHit = FirstHit;
            this.shipLength = shipLength;
        }

        public Square NextTarget()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();

            var up = grid.GetAvailablePlacementsInDirection(firstHit, Grid.Direction.Upwards);
            var right = grid.GetAvailablePlacementsInDirection(firstHit, Grid.Direction.Rightwards);
            var down = grid.GetAvailablePlacementsInDirection(firstHit, Grid.Direction.Downwards);
            var left = grid.GetAvailablePlacementsInDirection(firstHit, Grid.Direction.Leftwards);

            if (up.Count() > 0)
            {
                squares.Add(up);
            }

            if (right.Count() > 0)
            {
                squares.Add(right);
            }

            if (down.Count() > 0)
            {
                squares.Add(down);
            }

            if (left.Count() > 0)
            {
                squares.Add(left);
            }

            var sorted = squares.OrderByDescending(seq => seq.Count());
            int maxLength = sorted.ElementAt(0).Count();

            if (maxLength > shipLength - 1)
            {
                maxLength = shipLength - 1;
            }

            var longest = sorted.Where(seq => seq.Count() >= maxLength);

            if (longest.Count() == 1)
            {
                return longest.ElementAt(0).First();
            }

            int index = random.Next(longest.Count());

            return longest.ElementAt(index).First();
        }

        private readonly Grid grid;
        private readonly int shipLength;
        private Square firstHit;
        private readonly Random random = new Random();
    }
}
