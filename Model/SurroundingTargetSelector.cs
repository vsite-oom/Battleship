using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

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
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
        
                var inDirection = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, direction);
                if (inDirection.Any())
                {
                    squares.Add(inDirection);
                }
            }
        var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Rightwards);
        if (right.Count() > 0)
        {
            squares.Add(right);
        }
        var down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Downwards);
        if (down.Count() > 0)
        {
            squares.Add(down);
        }
        var left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, Direction.Leftwards);
        if (left.Count() > 0)
        {
            squares.Add(left);
        }


        throw new NotImplementedException();
        int index = random.Next(squares.Count);
        return squares[index].First();
    }
}