namespace Model;

public class InlineTargetSelector : ITargetSelector
{
    public InlineTargetSelector(ShotsGrid grid, Square firstHit, Square secondHit)
    {
        this.grid = grid;
        this.firstHit = firstHit;
        current = secondHit;
        direction = DirectionFromTo(firstHit, secondHit);
    }

    private readonly ShotsGrid grid;
    private readonly Square firstHit;
    private Square current;
    private Direction direction;
    private bool hasFlipped = false;

    public Square Next()
    {
        var candidates = grid.GetSquaresInDirection(current.Row, current.Column, direction);

        if (!candidates.Any() && !hasFlipped)
        {
            hasFlipped = true;
            direction = Opposite(direction);
            current = firstHit;
            candidates = grid.GetSquaresInDirection(current.Row, current.Column, direction);
        }

        if (candidates.Any())
        {
            current = candidates.First();
            return current;
        }

        return FallbackAnySquare();
    }

    private Square FallbackAnySquare()
    {
        var anyAvailable = grid.Squares.FirstOrDefault(s => s.SquareState == SquareState.Intact);
        return anyAvailable ?? current;
    }

    private static Direction DirectionFromTo(Square from, Square to)
    {
        if (to.Row < from.Row) return Direction.Upwards;
        if (to.Row > from.Row) return Direction.Downwards;
        if (to.Column < from.Column) return Direction.Leftwards;
        return Direction.Rightwards;
    }

    private static Direction Opposite(Direction direction)
    {
        return direction switch
        {
            Direction.Upwards => Direction.Downwards,
            Direction.Downwards => Direction.Upwards,
            Direction.Leftwards => Direction.Rightwards,
            Direction.Rightwards => Direction.Leftwards,
            _ => direction
        };
    }
}
