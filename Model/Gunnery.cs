using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum CurrentShootingTacttics
    {
        Random,
        Zone,
        Line
    }
    public class Gunnery
    {
        private readonly Grid grid;
        private readonly Fleet fleet;
        private IShootingTactics shootingTactics;
        public CurrentShootingTacttics CurrentShootingTacttics { get; set; }

        public Gunnery(GameRules rules, Fleet fleet)
        {

            grid = new Grid(rules.GridRows, rules.GridColumns);
            this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
        }
        public Square nextTarget(Square square)
        {
            return shootingTactics.NextTarget();
        }

        public void ProcessHitResult(HitResult result)
        {

            CurrentShootingTacttics = (CurrentShootingTacttics, result) switch
            {
                (CurrentShootingTacttics.Random, HitResult.Hit) => CurrentShootingTacttics.Zone,
                (CurrentShootingTacttics.Zone, HitResult.Hit) => CurrentShootingTacttics.Line,
                (CurrentShootingTacttics.Zone, HitResult.Sunk) or (CurrentShootingTacttics.Line, HitResult.Sunk) => CurrentShootingTacttics.Random,
                _ => CurrentShootingTacttics
            };
            //if (CurrentShootingTacttics == CurrentShootingTacttics.Random&& result == HitResult.Hit)
            //{
            //        CurrentShootingTacttics = CurrentShootingTacttics.Zone;
            //}
            //else if (CurrentShootingTacttics == CurrentShootingTacttics.Zone)
            //{
            //    if (result == HitResult.Hit)
            //    {
            //        CurrentShootingTacttics = CurrentShootingTacttics.Line;
            //    }
            //    else if (result == HitResult.Sunk)
            //    {
            //        CurrentShootingTacttics = CurrentShootingTacttics.Random;
            //    }
            //}
            //else if (CurrentShootingTacttics == CurrentShootingTacttics.Line&& result == HitResult.Sunk)
            //{
            //    CurrentShootingTacttics = CurrentShootingTacttics.Random;
            //}
        }
    }
}
