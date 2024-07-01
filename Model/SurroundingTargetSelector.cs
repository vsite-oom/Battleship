using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace vsite.oom.battleship.model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength) 
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLength = shipLength;
        }
        private readonly ShotsGrid grid;
        private readonly Square firstHit;
        private readonly int shipLength;
        private readonly Random random = new Random();
        public Square Next()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            var up = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Upwards);
            if (up.Count() > 0)
                squares.Add(up);
            var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Rightwards);
            if (right.Count() > 0)
                squares.Add(right);
            var down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Downwards);
            if (down.Count() > 0)
                squares.Add(down);
            var left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Leftwards);
            if (left.Count() > 0)
                squares.Add(left);
            int index = random.Next(squares.Count());
            return squares.ElementAt(index).First();
        }
    }
}
