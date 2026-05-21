namespace Battleship.Model;

/// <summary>Selects a target square adjacent to the first confirmed hit, used when the ship axis is not yet known.</summary>
public class SurroundingTargetSelector : ITargetSelector
{
    private readonly ShotsGrid grid;
    private readonly Square firstHit;
    private readonly Random random = new Random();

    /// <summary>Initializes a new instance of the <see cref="SurroundingTargetSelector"/> class.</summary>
    /// <param name="grid">The shots grid used to find available adjacent squares.</param>
    /// <param name="firstHit">The square of the first confirmed hit.</param>
    public SurroundingTargetSelector(ShotsGrid grid, Square firstHit)
    {
        this.grid = grid;
        this.firstHit = firstHit;
    }

    /// <inheritdoc/>
    public Square Next()
    {
        List<IEnumerable<Square>> squares = [];

        foreach (Direction direction in Enum.GetValues<Direction>())
        {
            var inDirection = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, direction);
            if (inDirection.Any())
            {
                squares.Add(inDirection);
            }
        }

        int index = random.Next(squares.Count);
        return squares[index].First();
    }
}
