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
            shipLenghts = new List<int>(gameRules.ShipLengths);
            ChangeToRandom();
            
            
        }

        public Square nextTarget()
        {
            LastTarget = shootingTactics.NextTarget();
            return LastTarget;
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
            shootingTactics = new LineShooting(grid, hitSquares, shipLenghts);
            currentShootingTactics = CurrentShootingTactics.Line;
            //TODO: Apply actual tactics
        }

        private void ChangeToZone()
        {
            shootingTactics = new ZoneShooting(grid, LastTarget, shipLenghts);
            currentShootingTactics = CurrentShootingTactics.Zone;
        }

        private void ChangeToRandom()
        {
            shootingTactics = new RandomShooting(grid, shipLenghts);
            currentShootingTactics = CurrentShootingTactics.Random;
        }

        private void RecordHitResult(HitResult hitResult)
        {
            if (hitResult != HitResult.Missed)
            {
                hitSquares.Add(LastTarget);
            }

            if (hitResult == HitResult.Sunk)
            {
                foreach (var square in hitSquares)
                {
                    grid.MarkSquare(square.Row, square.Column, HitResult.Sunk);
                }
                shipLenghts.Remove(hitSquares.Count);
                hitSquares.Clear();
            }
            else
            {
                
                grid.MarkSquare(LastTarget.Row, LastTarget.Column, hitResult);
            }

        }

        List<Square> hitSquares = new List<Square>();
        Square LastTarget;

        private readonly Grid grid;
        private List<int> shipLenghts;


        private IShootingTactics shootingTactics;

        public CurrentShootingTactics currentShootingTactics { get; private set; }
    }
}
