using System.Diagnostics;

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
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            shipLengths = new List<int>(gameRules.ShipLengths);
            ChangeToRandom();
        }

        public Square NextTarget()
        {
            lastTarget = shootingTactics.NextTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            //RecordHitResult(hitResult);
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

        private readonly Grid grid;
        private List<int> shipLengths;

        private IShootingTactics shootingTactics;

        List<Square> hitSquares = new List<Square>();
        Square lastTarget;

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
