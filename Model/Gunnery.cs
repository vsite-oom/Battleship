﻿using System.Data.Common;
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
    private readonly FleetGrid _recordFleetGrid;
    private Square? target;
    private ITargetSelector targetSelector;
    private readonly List<int> shipLengths = [];

    public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
    {
        _recordFleetGrid = new FleetGrid(rows, columns);
        this.shipLengths = new List<int>(shipLengths.OrderDescending());

        targetSelector = new RandomTargetSelector(_recordFleetGrid, this.shipLengths[0]);
    }

    public ShootingTactics ShootingTactics { get; private set; } = ShootingTactics.Random;

    public Square Next()
    {
        target = targetSelector.Next();
        return target;
    }

    public void ProcessHitResult(HitResult hitResult)
    {
        
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

    private void ChangeTacticsToRandom()
    {
        ShootingTactics = ShootingTactics.Random;
        targetSelector = new RandomTargetSelector(_recordFleetGrid, this.shipLengths[0]);
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