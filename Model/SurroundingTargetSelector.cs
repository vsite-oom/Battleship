namespace Vsite.Oom.Battleship.Model;

public class SurroundingTargetSelector : ITargetSelector
{
    private readonly ShotsGrid _grid;
    private readonly Square _firstHit;
    private readonly int _shipLength;

    public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength)
    {
        _grid = grid;
        _firstHit = firstHit;
        _shipLength = shipLength;
    }

    public Square Next()
    {
        List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var squaresInDirection = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, direction);
            if(squaresInDirection.Count() > 0 )
                squares.Add(squaresInDirection);
        }

        var longest = squares.OrderByDescending(s => s.Count()).First();

        return longest.First();
    }

    public void ProcesshitResult(HitResult hitResult)
    {
        throw new NotImplementedException();
    }
}