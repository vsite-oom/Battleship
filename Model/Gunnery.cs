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
            shootingTactics = new RandomShooting(grid);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square NextTarget()
        {
            targetSquares.Add(shootingTactics.NextTarget());
            return targetSquares.Last();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            //RecordHitResult(hitResult);
            ChangeTactics(hitResult);
        }
        private void RecordHitResult(HitResult hitResult)
        {
            var lastTarget = targetSquares.Last();
            grid.MarkSquare(lastTarget.Row, lastTarget.Column, hitResult);
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
            CurrentShootingTactics = CurrentShootingTactics.Line;
            // TODO: Apply actual tactics
        }

        private void ChangeToZone()
        {
            CurrentShootingTactics = CurrentShootingTactics.Zone;
            // TODO: Apply actual tactics
        }

        private void ChangeToRandom()
        {
            CurrentShootingTactics = CurrentShootingTactics.Random;
            // TODO: Apply actual tactics
        }

        private readonly Grid grid;
        private IShootingTactics shootingTactics;

        List<Square> targetSquares = new List<Square>();

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
