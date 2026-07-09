namespace Model;

public class Gunnery
{
    private readonly ShotsGrid _grid;
    private readonly List<int> _shipLengths;
    private readonly SquareEliminator _eliminator = new();
    private readonly List<Square> _shipSquares = new();
    private ITargetSelector _targetSelector;
    private Square _target = null!;

    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        ArgumentNullException.ThrowIfNull(shipLengths);
        _grid = new ShotsGrid(rows, columns);
        _shipLengths = new List<int>(shipLengths.OrderDescending());
        _targetSelector = new RandomTargetSelector(_grid, _shipLengths[0]);
    }

    public Square Next()
    {
        _target = _targetSelector.Next();
        return _target;
    }

    public void ProcessHitResult(HitResult hitResult)
    {
        RecordResult(hitResult);

        switch (hitResult)
        {
            case HitResult.Missed:
                return;
            case HitResult.Hit:
                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ChangeTacticsToSurrounding();
                        return;
                    case ShootingTactics.Surrounding:
                        ChangeTacticsToInline();
                        return;
                    case ShootingTactics.Inline:
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected tactics: {ShootingTactics}");
                }
            case HitResult.Sunken:
                ChangeTacticsToRandom();
                return;
        }
    }

    private void RecordResult(HitResult hitResult)
    {
        switch (hitResult)
        {
            case HitResult.Missed:
                _target.ChangeState(SquareState.Missed);
                return;
            case HitResult.Hit:
                _target.ChangeState(SquareState.Hit);
                _shipSquares.Add(_target);
                return;
            case HitResult.Sunken:
                MarkShipSunken();
                return;
        }
    }

    private void MarkShipSunken()
    {
        _shipSquares.Add(_target);
        foreach (var sq in _shipSquares)
        {
            sq.ChangeState(SquareState.Sunken);
        }
        var toEliminate = _eliminator.ToEliminate(_shipSquares, _grid.Rows, _grid.Columns);
        foreach (var coord in toEliminate)
        {
            _grid.ChangeSquareState(coord.Row, coord.Column, SquareState.Eliminated);
        }
        _shipSquares.Clear();
    }

    private void ChangeTacticsToRandom()
    {
        ShootingTactics = ShootingTactics.Random;
        _targetSelector = new RandomTargetSelector(_grid, _shipLengths[0]);
    }

    private void ChangeTacticsToSurrounding()
    {
        ShootingTactics = ShootingTactics.Surrounding;
        _targetSelector = new SurroundingTargetSelector(_grid, _target);
    }

    private void ChangeTacticsToInline()
    {
        ShootingTactics = ShootingTactics.Inline;
        _targetSelector = new InlineTargetSelector(_grid, _shipSquares.ToList());
    }
}
