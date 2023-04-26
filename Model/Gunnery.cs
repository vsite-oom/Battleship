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
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            shipLenghts = new List<int>(gameRules.ShipLenghts);
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
                foreach (var square in hitSquares)
                {
                    grid.MarkSquare(square.Row, square.Column, hitResult);
                }
                shipLenghts.Remove(hitSquares.Count);
                hitSquares.Clear();
            }

            else
            {

                grid.MarkSquare(hitSquares.Last().Row, hitSquares.Last().Column, hitResult);
            }

            var lastTarget = hitSquares.Last();
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
                    {
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
                    }
                    break;
                default:
                    Debug.Assert(false, "Unsupported hit result");
                    break;
            }
        }

        private void ChangeToRandom()
        {
           
            shootingTactics = new RandomShooting(grid, shipLenghts);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        private void ChangeToLine()
        {
            shootingTactics = new LineShooting(grid,hitSquares, shipLenghts);
            CurrentShootingTactics = CurrentShootingTactics.Line;
            
        }

        private void ChangeToZone()
        {
            shootingTactics = new ZoneShooting(grid, lastTarget, shipLenghts);
            CurrentShootingTactics = CurrentShootingTactics.Zone;
        }

        

        private readonly Grid grid;

        private List<int> shipLenghts;

        private IShootingTactics shootingTactics;

        List<Square> hitSquares = new List<Square>();
        Square lastTarget;


        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
