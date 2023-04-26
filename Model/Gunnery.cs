using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
            currentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square nextTarget()
        {
            targetSquares.Add(shootingTactics.NextTarget());
            return targetSquares.Last();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
           // RecordHitResult(hitResult);
            ChangeTactics(hitResult);



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
                    {
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
                                Debug.Assert(false, "Unsuported shooting tactics");
                                break;
                        }
                    }
                    break;
                default:
                    Debug.Assert(false, "Unsuported shooting tactics");
                    break;

            }
        }

        private void ChangeToLine()
        {
            currentShootingTactics = CurrentShootingTactics.Line;
            //TODO: Apply actual tactics
        }

        private void ChangeToZone()
        {
            currentShootingTactics = CurrentShootingTactics.Zone;
            //TODO: Apply actual tactics
        }

        private void ChangeToRandom()
        {
            throw new NotImplementedException();
        }

        private void RecordHitResult(HitResult hitResult)
        {
            if (hitResult == HitResult.Sunk)
            {
                foreach (var square in targetSquares)
                {
                    grid.MarkSquare(square.Row, square.Column, HitResult.Sunk);
                }
                targetSquares.Clear();
            }
            else
            {
                var lastTarget = targetSquares.Last();
                grid.MarkSquare(lastTarget.Row, lastTarget.Column, hitResult);
            }

        }

        List<Square> targetSquares = new List<Square>();

        private readonly Grid grid;
        private readonly Fleet fleet;
        private IShootingTactics shootingTactics;

        public CurrentShootingTactics currentShootingTactics { get; private set; }
    }
}
