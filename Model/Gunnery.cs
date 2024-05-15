using System.Data.Common;
using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model;

public enum ShootingTactics
{
    Random,
    Surrounding,
    Inline
}

public class Gunnery
{
    private readonly ShotsGrid _recordGrid;
    private Square? target;
    private ITargetSelector targetSelector;
    private readonly List<int> shipLengths = [];
    private List<Square> shipSquares = [];
    private SquareEliminator SquareEliminator = new SquareEliminator();

    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        _recordGrid = new ShotsGrid(rows, columns);
        this.shipLengths = new List<int>(shipLengths.OrderDescending());

        targetSelector = new RandomTargetSelector(_recordGrid, this.shipLengths[0]);
    }

    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    public Square Next()
    {
        target = targetSelector.Next();
        return target;
    }

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
                        Debug.Assert(false);
                        return;
                }
            case HitResult.Sunken:
                ChangeTacticsToRandom();
                return;
        }
    }

    private void RecordTargetResult(HitResult hitResult)
    {
        switch (hitResult)
        {
            case HitResult.Missed: target?.ChangeState(SquareState.Missed);
                return;
            case HitResult.Hit: target?.ChangeState((SquareState.Hit));
                shipSquares.Add(target);
                return;
            case HitResult.Sunken: MarkShipSunken();
                return;
        }
        
    }

    private void MarkShipSunken()
    {
        shipSquares.Add(target);
        foreach (var square in shipSquares)
        {
            square.ChangeState(SquareState.Sunken);
        }

        var toEliminate = SquareEliminator.ToEliminate(shipSquares, _recordGrid.Rows, _recordGrid.Columns);
        foreach (var square in toEliminate)
        {
            _recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Eliminated);
        }
        
        shipSquares.Clear();
    }

    private void ChangeTacticsToRandom()
    {
        ShootingTactics = ShootingTactics.Random;
        targetSelector = new RandomTargetSelector(_recordGrid, this.shipLengths[0]);
    }

    private void ChangeTacticsToSurrounding()
    {
        ShootingTactics = ShootingTactics.Surrounding;
        targetSelector = new SurroundingTargetSelector();
    }

    private void ChangeTacticsToInline()
    {
        ShootingTactics = ShootingTactics.Inline;
        targetSelector = new InlineTargetSelector();
    }
}