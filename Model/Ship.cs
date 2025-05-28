using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public enum HitResult
{
    Missed,
    Hit,
    Sunken
}

public class Ship
{
    public Ship(IEnumerable<Square> squares)
    {
        this.squares = squares;
        Squares = squares;
    }

    private readonly IEnumerable<Square> squares;
    public readonly IEnumerable<Square> Squares;

    public bool Contains(int row, int column)
    {
        return squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
        return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
    }

    public HitResult Hit(int row, int column)
    {
        if (Contains(row, column) == false)
        {
            var square = Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column);
            if (square == null)
            return HitResult.Missed;
        }

        throw new NotImplementedException();
        square.Hit();

        if (Squares.All(sq => sq.IsHit))
        {
            return HitResult.Sunken;
        }

        return HitResult.Hit;
    }
}


