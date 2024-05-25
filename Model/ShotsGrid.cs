namespace Vsite.Oom.Battleship.Model;

public enum Direction
{
    Upwards,
    Downwards,
    Left,
    Right
}

public class ShotsGrid : Grid
{
    public ShotsGrid(int rows, int columns) : base(rows, columns) { }

    public override bool IsSquareAvailable(int row, int column)
    {
        return _squares[row, column]?.SquareState == SquareState.Intact;
    }

    public Square GetSquare(int row, int column)
    {
        return _squares[row, column]!;
    }

    public void ChangeSquareState(int row, int column, SquareState newState)
    {
        _squares[row, column]!.ChangeState(newState);
    }

    public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction newDirection)
    {
        //Todo: GetSquaresInDirection <,>,|;
        throw new NotImplementedException();
    }
}