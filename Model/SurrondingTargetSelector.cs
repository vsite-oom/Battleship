using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class SurrondingTargetSelector : ITargetSelector
    {
        private readonly ShotsGrid grid;
        private readonly Square firstHit;
        private readonly int shipLen;
        public  SurrondingTargetSelector(ShotsGrid grid,Square firstHit,int shipLen) {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLen = shipLen;
        }
        public Square Next()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            var up=grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Upwards);
            var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Right);
            var left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Left);
            var down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Downwards);
            if(up.Count()>0) squares.Add(up);
            if(right.Count()>0) squares.Add(right);
            if(left.Count()>0) squares.Add(left);
            if(down.Count()>0) squares.Add(down);
            throw new NotImplementedException();
        }
    }
}
