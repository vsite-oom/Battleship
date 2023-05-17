using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum CurrentShootingTactics
    {
        Random,
        Zone,
        Line
    }

    public class Gunnery
    {
        public Gunnery(GameRules gameRules)
        {
            this.gameRules = gameRules;
            grid = new RecordGrid(gameRules.GridRows, gameRules.GridColumns);
            shipLengths = new List<int>(gameRules.ShipLengths);
            ChangeToRandom();
        }

        GameRules gameRules;

        public Square NextTarget()
        {
            lastTarget = shootingTactics.NextTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            RecordHitResult(hitResult);
            ChangeTactics(hitResult);
        }
        private void RecordHitResult(HitResult hitResult)
        {
            if (hitResult != HitResult.Missed)
            {
                hitSquares.Add(lastTarget);
            }
            if (hitResult == HitResult.Sunk)
            {
                var toEliminate = gameRules.Terminator.ToEliminate(hitSquares);
                foreach (var square in toEliminate)
                {
                    grid.Eliminate(square.Row, square.Column);
                }
                foreach (var square in hitSquares)
                {
                    grid.MarkSquare(square.Row, square.Column, HitResult.Sunk);
                }
                shipLengths.Remove(hitSquares.Count);
                hitSquares.Clear();
            }
            else
            {
                grid.MarkSquare(lastTarget.Row, lastTarget.Column, hitResult);
            }
        }

        private void ChangeTactics(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunk:
                    ChangeToRandom();
                    return;
                case HitResult.Hit:
                    switch (CurrentShootingTactics)
                    {
                        case CurrentShootingTactics.Random:
                            ChangeToZone();
                            return;
                        case CurrentShootingTactics.Zone:
                            ChangeToLine();
                            return;
                        case CurrentShootingTactics.Line:
                            return;
                        default:
                            Debug.Assert(false, "Unsupported shooting tactics");
                            break;
                    }
                    break;
                default:
                    Debug.Assert(false, "Unsupported hit result");
                    break;
            }
        }

        private void ChangeToLine()
        {
            shootingTactics = new LineShooting(grid, hitSquares, shipLengths);
            CurrentShootingTactics = CurrentShootingTactics.Line;
        }

        private void ChangeToZone()
        {
            shootingTactics = new ZoneShooting(grid, lastTarget, shipLengths);
            CurrentShootingTactics = CurrentShootingTactics.Zone;
        }

        private void ChangeToRandom()
        {
            shootingTactics = new RandomShooting(grid, shipLengths);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        private readonly RecordGrid grid;
        private readonly List<int> shipLengths;

        private IShootingTactics shootingTactics;

        List<Square> hitSquares = new List<Square>();
        Square lastTarget;

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}