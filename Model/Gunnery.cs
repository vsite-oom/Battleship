using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private readonly Grid grid;
        private List<int> shipLenghts;
        private readonly List<Square> targets = new();
        public IShootingTactics shootingTactics;
        public GameRules gameRules;
        Square target;

        public Gunnery(GameRules rules)
        {
            this.gameRules = rules;
            grid = new(gameRules.gridRows, gameRules.gridColumns);
            shipLenghts = new List<int>(gameRules.shipLenghts);
            CurrentShootingTactics = CurrentShootingTactics.Random;
            ChangeTo(CurrentShootingTactics.Random);
        }

        public Square NextTarget()
        {

            target = shootingTactics.AddNextTarget();
            return target;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            // RecordHitResult(hitResult);
            ChangeTactic(hitResult);
        }

        private void ChangeTactic(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    break;
                case HitResult.Sank:
                    ChangeTo(CurrentShootingTactics.Random);
                    break;
                case HitResult.Hit:
                    switch(CurrentShootingTactics)
                    {
                        case CurrentShootingTactics.Random:
                            ChangeTo(CurrentShootingTactics.Zone);
                            break;
                        case CurrentShootingTactics.Line:
                            break;
                        case CurrentShootingTactics.Zone:
                            ChangeTo(CurrentShootingTactics.Line);
                            break;
                        default:
                            Debug.Assert(false, "Tactic not supported");
                            break;
                    }
                    break;
                default:
                    Debug.Assert(false, "HitResult not supported");
                    break;
            }
        }

        private void RecordHitResult(HitResult hitResult)
        {
            if(hitResult != HitResult.Missed)
            {
                targets.Add(target);
            }
            if(hitResult == HitResult.Sank)
            {
                var toEliminate = gameRules.terminator.ToEliminate(targets);
                foreach(var sq in toEliminate)
                {
                    grid.Eliminate(sq.Row, sq.Column);
                }
                foreach(var sq in targets)
                {
                    grid.MarkSquare(sq.Row, sq.Column, hitResult);
                }

                shipLenghts.Remove(targets.Count());
                targets.Clear();
            }
            else
            {
                grid.MarkSquare(target.Row, target.Column, hitResult);
            }
            var prevTarget = targets.Last();
            grid.MarkSquare(prevTarget.Row, prevTarget.Column, hitResult);
        }

        private void ChangeTo(CurrentShootingTactics newTactic)
        {
            CurrentShootingTactics = newTactic;
            switch(newTactic)
            {
                case CurrentShootingTactics.Line:
                    shootingTactics = new LineShooting(grid, targets, shipLenghts);
                    break;
                case CurrentShootingTactics.Zone:
                    shootingTactics = new ZoneShooting(grid, target, shipLenghts);
                    break;
                case CurrentShootingTactics.Random:
                    shootingTactics = new RandomShooting(grid, shipLenghts);
                    break;
                default:
                    Debug.Assert(false, "Tactic change not supported");
                    break;
            }
        }

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
