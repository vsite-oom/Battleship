using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public class LinearShooting : ISelectTarget
    {
        public LinearShooting(Grid grid, IEnumerable<Square> alreadyHit, int shipLength)
        {
            this.grid = grid;
            squaresHit = new List<Square>(alreadyHit);
            this.shipLength = shipLength;
            this.orientation = GetOrientation();
        }

        private Orientation GetOrientation()
        {
            return squaresHit.First().Row == squaresHit.Last().Row ? Orientation.Horizontal : Orientation.Vertical;
        }

        public Square NextTarget()
        {
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            var sorted = SortSquares(squaresHit);
            var firstSquare = sorted.First();
            var lastSquare = sorted.Last();
            switch (orientation)
            {
                case Orientation.Horizontal:
                    var left = grid.GetSequence(firstSquare, Direction.Left);
                    if (left.Count() > 0)
                        squares.Add(left);
                    var right = grid.GetSequence(lastSquare, Direction.Right);
                    if (right.Count() > 0)
                        squares.Add(right);
                    break;
                case Orientation.Vertical:
                    var up = grid.GetSequence(firstSquare, Direction.Up);
                    if (up.Count() > 0)
                        squares.Add(up);
                    var down = grid.GetSequence(lastSquare, Direction.Down);
                    if (down.Count() > 0)
                        squares.Add(down);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            // sort list from longest to shortest
            var ordered = squares.OrderByDescending(seq => seq.Count());
            int maxLen = squares.First().Count();
            if (maxLen > shipLength - squaresHit.Count())
                maxLen = shipLength - squaresHit.Count();
            // filter out all arrays for which length is equal or larger than shipLength - 1
            var longestCandidates = ordered.Where(seq => seq.Count() >= maxLen);
            int index = random.Next(longestCandidates.Count());
            return longestCandidates.ElementAt(index).First();
        }

        private List<Square> SortSquares(List<Square> squares)
        {
            return squares.OrderBy(s => s.Column + s.Row).ToList();
        }

        private Grid grid;
        private List<Square> squaresHit;
        private int shipLength;
        private Orientation orientation;
        private Random random = new Random();
    }
}
