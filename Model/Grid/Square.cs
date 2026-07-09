namespace Model;

public record struct Coordinate(int Row, int Column);

public class Square
{
    public Coordinate Position { get; }
    public SquareState State { get; private set; } = SquareState.None;

    public Square(int row, int column)
    {
        Position = new Coordinate(row, column);
    }

    public void ChangeState(SquareState state)
    {
        if (state > State)
        {
            State = state;
        }
    }
}
