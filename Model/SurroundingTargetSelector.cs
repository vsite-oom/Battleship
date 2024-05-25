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
        var up = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Upwards);
        if (up.Count() > 0) squares.Add(up);
        var left= _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Left);
        if (left.Count() > 0) squares.Add(left);
        var right = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Right);
        if (right.Count() > 0) squares.Add(right);
        var down = _grid.GetSquaresInDirection(_firstHit.Row, _firstHit.Column, Direction.Downwards);
        if (down.Count() > 0) squares.Add(down);



        throw new NotImplementedException();
    }

    public void ProcesshitResult(HitResult hitResult)
    {
        throw new NotImplementedException();
    }
}