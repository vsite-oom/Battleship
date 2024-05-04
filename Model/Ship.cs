namespace Vsite.Oom.Battleship.Model;

public enum HitResult
{
    Missed,
    Hit,
    Sunken
}

public class Ship
{
    public readonly IEnumerable<Square> Squares;

    public Ship(IEnumerable<Square> squares)
    {
        Squares = squares;
    }

    public bool Contains(int row, int column)
    {
        return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
    }

    public HitResult Hit(int row, int column)
    {
        if (!Contains(row, column)) return HitResult.Missed;

        throw new NotImplementedException();
    }
}