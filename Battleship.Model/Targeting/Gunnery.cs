namespace Battleship.Model;

/// <summary>Specifies the shooting tactic currently used by the gunnery system.</summary>
public enum ShootingTactics
{
    /// <summary>Fires at random available squares.</summary>
    Random,
    /// <summary>Fires at squares adjacent to the first confirmed hit.</summary>
    Surrounding,
    /// <summary>Fires along the axis of the last two confirmed hits.</summary>
    Inline
}

/// <summary>Manages the targeting logic for an AI player, selecting squares to fire at and adapting tactics based on hit results.</summary>
public class Gunnery
{
    /// <summary>Initializes a new instance of the <see cref="Gunnery"/> class.</summary>
    /// <param name="rows">The number of rows in the shot-recording grid.</param>
    /// <param name="columns">The number of columns in the shot-recording grid.</param>
    /// <param name="shipLengths">The lengths of all ships in the opposing fleet, used to optimize targeting.</param>
    /// <exception cref="ArgumentNullException"><paramref name="shipLengths"/> is <see langword="null"/>.</exception>
    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        ArgumentNullException.ThrowIfNull(shipLengths);
        recordGrid = new ShotsGrid(rows, columns);
        this.shipLengths = new List<int>(shipLengths.OrderDescending());
        targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
    }

    /// <summary>Gets the current shooting tactics.</summary>
    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    private readonly ShotsGrid recordGrid;
    private readonly List<int> shipLengths;
    private readonly SquareEliminator eliminator = new SquareEliminator();

    // null! is safe: Next() must always be called before ProcessHitResult()
    private Square target = null!;
    private List<Square> shipSquares = [];
    private ITargetSelector targetSelector;

    /// <summary>Selects and returns the next target square.</summary>
    /// <returns>The square to fire at next.</returns>
    public Square Next()
    {
        target = targetSelector.Next();
        return target;
    }

    /// <summary>Updates the gunnery state based on the result of the last shot and adjusts tactics if necessary.</summary>
    /// <param name="hitResult">One of the enumeration values that specifies the result of the last shot.</param>
    public void ProcessHitResult(HitResult hitResult)
    {
        RecordTargetResult(hitResult);
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
                        throw new InvalidOperationException($"Unexpected ShootingTactics value: {ShootingTactics}");
                }
            case HitResult.Sunken:
                ChangeTacticsToRandom();
                return;
            default:
                throw new InvalidOperationException($"Unexpected HitResult value: {hitResult}");
        }
    }

    private void RecordTargetResult(HitResult hitResult)
    {
        switch (hitResult)
        {
            case HitResult.Missed:
                target.ChangeState(SquareState.Missed);
                return;
            case HitResult.Hit:
                target.ChangeState(SquareState.Hit);
                shipSquares.Add(target);
                return;
            case HitResult.Sunken:
                MarkShipSunken();
                return;
            default:
                throw new InvalidOperationException($"Unexpected HitResult value: {hitResult}");
        }
    }

    private void MarkShipSunken()
    {
        shipSquares.Add(target);
        foreach (var square in shipSquares)
        {
            square.ChangeState(SquareState.Sunken);
        }
        var toEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
        foreach (var square in toEliminate)
        {
            recordGrid.ChangeSquareState(square.Row, square.Column, SquareState.Eliminated);
        }
        shipSquares.Clear();
    }

    private void ChangeTacticsToRandom()
    {
        ShootingTactics = ShootingTactics.Random;
        targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
    }

    private void ChangeTacticsToSurrounding()
    {
        ShootingTactics = ShootingTactics.Surrounding;
        targetSelector = new SurroundingTargetSelector(recordGrid, target);
    }

    private void ChangeTacticsToInline()
    {
        ShootingTactics = ShootingTactics.Inline;
        targetSelector = new InlineTargetSelector();
    }
}
