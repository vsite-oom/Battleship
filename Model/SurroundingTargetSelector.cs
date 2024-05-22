// Ignore Spelling: Vsite Oom
using System.ComponentModel.DataAnnotations;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingTargetSelector : ITargetSelector
    {
        public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength) 
        { 
            _grid = grid;
            _firstHit = firstHit;
            _shipLength = shipLength;
        }

        //TODO: Implement shoting target selector
        public Square Next()
        {
            List<IEnumerable<Square>> _squares = new List<IEnumerable<Square>>();
            //TODO: Implement with a loop

            var up = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Upwards);
            if(up.Count() > 0) { _squares.Add(up); }

            var right = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Rightwards);
            if(right.Count() > 0) { _squares.Add(right); }

            var down = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Downwards);
            if (down.Count() > 0) { _squares.Add(down); }

            var left = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Leftwards);
            if (left.Count() > 0) { _squares.Add(left); }

            throw new NotImplementedException();
        }

        private readonly ShotsGrid _grid;
        private readonly Square _firstHit;
        private readonly int _shipLength;
    }
}
