using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;
using System.Diagnostics;


public enum ShootingTactics
{
    Random,
    Surrounding,
    Inline
}

public class Gunnery
{
    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        recordGrid = new FleetGrid(rows, columns);
        recordGrid = new ShotsGrid(rows, columns);
        this.shipLengths = new List<int>(shipLengths.OrderDescending());
        targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
    }

    public SquareCoordinate Next()
         public Square Next()
    {
        throw new NotImplementedException();
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
                        ChangeTacticsToInline(); break;
                        ChangeTacticsToInline();
                        return;
                    case ShootingTactics.Inline:
                        return;
                    default:
                        Debug.Assert(false);
                        return;
                }
                return;
            case HitResult.Sunken:
                ChangeTacticsToRandom();
                return;
        }

    }

    private void ChangeTacticsToRandom()
    {
        ShootingTactics = ShootingTactics.Random;
        targetSelector = new RandomTargetSelector();
        targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
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
            recordGrid.GetSquare(square.Row, square.Column).ChangeState(SquareState.Eliminated);
        }
        shipSquares.Clear();
    }


    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    private readonly FleetGrid recordGrid;

    private readonly List<int> shipLengths = [];
    private List<Square> shipSquares = new List<Square>();

    private Square target;

    private ITargetSelector targetSelector = new RandomTargetSelector();
    private ITargetSelector targetSelector;
    private readonly SquareEliminator eliminator = new SquareEliminator();
}