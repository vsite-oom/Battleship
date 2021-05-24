using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Vsite.Oom.Battleship.Model
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public class LinearShooting : ITargetSelect
    {
        public LinearShooting(Grid evidenceGrid, IEnumerable<Square> squaresHit, int shipLengths)
        {

            this.grid = evidenceGrid;
            this.squaresHit = new List<Square>(squaresHit.OrderBy(s => s.Row + s.Column));
            this.shipLengths = shipLengths;
        }

        public Square NextTarget()
        {
            var orientation = GetHitSquaresOrientation();
            List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
            switch (orientation)
            {
                case Orientation.Horizontal:
                    var left = grid.GetAvailablePlacementsInDirection(squaresHit[0], Grid.Direction.Leftwards);

                    if (left.Count() > 0)
                    {
                        squares.Add(left);
                    }

                    var right = grid.GetAvailablePlacementsInDirection(squaresHit[1], Grid.Direction.Rightwards);

                    if (right.Count() > 0)
                    {
                        squares.Add(right);

                    }
                    break;
                case Orientation.Vertical:
                    var up = grid.GetAvailablePlacementsInDirection(squaresHit[0], Grid.Direction.Upwards);

                    if (up.Count() > 0)
                    {
                        squares.Add(up);
                    }

                    var down = grid.GetAvailablePlacementsInDirection(squaresHit[1], Grid.Direction.Downwards);

                    if (down.Count() > 0)
                    {
                        squares.Add(down);
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            if (squares.Count > 1)
            {
                return squares[random.Next(0, 2)].First();
            }

            return squares[0].First();

            // TODO 6: select one of them optionally using random generator
        }

        private Orientation GetHitSquaresOrientation()
        {
            if (squaresHit[0].Row == squaresHit[1].Row)
            {
                return Orientation.Horizontal;
            }

            return Orientation.Vertical;
        }

    private Grid grid;
    private List<Square> squaresHit;
    private Random random = new Random();
    private readonly int shipLengths;
    }
}