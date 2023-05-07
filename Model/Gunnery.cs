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
        private readonly IShootingTactics shootingTactics;
        private readonly List<Square> targets = new();

        public Gunnery(GameRules rules)
        {
            grid = new(rules.gridRows, rules.gridColumns);
            shootingTactics = new RandomShooting(grid);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square NextTarget()
        {
            targets.Add(shootingTactics.AddNextTarget());
            return targets.Last();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            RecordHitResult(hitResult);
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
            var prevTarget = targets.Last();
            grid.MarkSquare(prevTarget.Row, prevTarget.Column, hitResult);
        }

        private void ChangeTo(CurrentShootingTactics newTactic)
        {
            CurrentShootingTactics = newTactic;
            // TODO: finish
        }

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
