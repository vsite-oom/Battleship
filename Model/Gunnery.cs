using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunnery
    {
        public enum CurrentShootingTactics
        {
            Random,
            Zone,
            Line
        }

        public Gunnery(GameRules gameRules)
        {
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            shootingTactics = new RandomShooting(grid);
            currentShootingTactics = CurrentShootingTactics.Random;
            shipLengths = new List<int>(gameRules.ShipLengts);
        }

        public Square nextTarget()
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
                var last = hitSquares.Last();
                grid.MarkSquare(last.Row, last.Column, hitResult);
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
                    switch (currentShootingTactics)
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
            currentShootingTactics = CurrentShootingTactics.Line;
        }

        private void ChangeToZone()
        {
            currentShootingTactics = CurrentShootingTactics.Zone;
        }

        private void ChangeToRandom()
        {
            currentShootingTactics = CurrentShootingTactics.Random;
        }

        private readonly Grid grid;
        private List<int> shipLengths;
        private IShootingTactics shootingTactics;

        List<Square> hitSquares = new List<Square>();
        Square lastTarget;

        public CurrentShootingTactics currentShootingTactics { get; private set; }
    }
}
