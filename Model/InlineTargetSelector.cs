using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    internal class InlineTargetSelector : ITargetSelector
    {
        private readonly ShotsGrid grid;
        private readonly IEnumerable<Square> squaresHit;
        private readonly int shipLength;
        public InlineTargetSelector(ShotsGrid grid,IEnumerable<Square> squaresHit,int shipLength) {
            this.grid = grid;
            this.squaresHit = squaresHit;
            this.shipLength = shipLength;
        }
        public Square Next()
        {
            var sorted = squaresHit.OrderBy(sq => sq.Row + sq.Column);
            var directionCandidates=new List<IEnumerable<Square>>();
            if (sorted.First().Row==sorted.Last().Row) {
                var left=grid.GetSquaresInDirection(sorted.First().Row,sorted.First().Column,Direction.Left);
                if (left!=null) {
                directionCandidates.Add(left);
                }
                var right=grid.GetSquaresInDirection(sorted.Last().Row,sorted.Last().Column,Direction.Right);
                if (right!=null) {
                directionCandidates.Add(right);
                }
            }
            else
            {
                var up = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
                if (up != null)
                {
                    directionCandidates.Add(up);
                }
                var down = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Downwards);
                if (down != null)
                {
                    directionCandidates.Add(down);
                }
            }
            return directionCandidates.OrderByDescending(ienum => ienum.Count()).ElementAt(0).ElementAt(0);
        }
    }
}
