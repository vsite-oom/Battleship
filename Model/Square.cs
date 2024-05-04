namespace Vsite.Oom.Battleship.Model;

public class Square
{
    public readonly int Column;

    public readonly int Row;

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool IsHit { get; private set; }

    public void Hit()
    {
        IsHit = true;
    }
}