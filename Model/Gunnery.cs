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
            RecordHitResult(hitResult);
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
                    ChangeToRandom1();
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
            CurrentShootingTactics = CurrentShootingTactics.Random;
            //TODO: apply actual tactics
        }

        private void ChangeToLine()
        {
            CurrentShootingTactics = CurrentShootingTactics.Line;
            //TODO: apply actual tactics
        }

        private void ChangeToZone()
        {
            CurrentShootingTactics = CurrentShootingTactics.Zone;
            //TODO: apply actual tactics
        }

        

        private readonly Grid grid;
        private IShootingTactics shootingTactics;

        List<Square> targetSquares = new List<Square>();


        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
