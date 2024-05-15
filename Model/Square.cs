namespace Vsite.Oom.Battleship.Model;

public enum SquareState
{
    Intact,
    Eliminated,
    Missed,
    Hit,
    Sunken
}
public class Square
{
    public readonly int Column;
    public readonly int Row;
    
    public SquareState SquareState { get; private set; }
    public bool IsHit => (int)SquareState >= (int)SquareState.Hit;

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        this.SquareState = SquareState.Intact;
    }
    
    public void Hit()
    {
        SquareState = SquareState.Hit;
    }

    public void ChangeState(SquareState newState)
    {
        if ((int)newState > (int)SquareState)
        {
            SquareState = newState;
        }
    }
}