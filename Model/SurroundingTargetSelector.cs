using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        private readonly ShotsGrid _grid;
        private readonly Square _firstHit;
        private readonly int _shipLength;

        public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength)
        {
            _grid = grid;
            _firstHit = firstHit;
            _shipLength = shipLength;
        }

        public Square Next()
        {
            List<IEnumerable<Square>> squares = [];


            var up = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Upwards);
            if (up.Count() > 0)
            {
                squares.Add(up);
            }

            var right = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Rightwards);
            if (right.Count() > 0)
            {
                squares.Add(right);
            }

            var down = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Downwards);
            if (down.Count() > 0)
            {
                squares.Add(down);
            }

            var left = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Leftwards);
            if (left.Count() > 0)
            {
                squares.Add(left);
            }

            // todo: implement the logic to select the next target

            throw new NotImplementedException();
        }
    }
}
