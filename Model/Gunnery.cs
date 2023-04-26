using System;
using System.Collections.Generic;
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

        public Gunnery(GameRules gameRules, Fleet fleet)
        {
            grid = new Grid(gameRules.GridRows, gameRules.GridColumns);
            this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
            currentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square nextTarget()
        {
            return shootingTactics.NextTarget();
        }

        public void ProcessHitResult(HitResult hitResult)
        {

            if (hitResult == HitResult.Sunk)
            {
                currentShootingTactics = CurrentShootingTactics.Random;
            }

            if (hitResult == HitResult.Hit && currentShootingTactics == CurrentShootingTactics.Zone)
            {
                currentShootingTactics = CurrentShootingTactics.Line;
            }

            if (hitResult == HitResult.Hit && currentShootingTactics == CurrentShootingTactics.Random)
            {
                currentShootingTactics = CurrentShootingTactics.Zone;
            }

            if (hitResult == HitResult.Hit && currentShootingTactics == CurrentShootingTactics.Line)
            {
                currentShootingTactics = CurrentShootingTactics.Line;
            }

            if (hitResult == HitResult.Missed && currentShootingTactics == CurrentShootingTactics.Random)
            {
                currentShootingTactics = CurrentShootingTactics.Random;
            }

            if (hitResult == HitResult.Missed && currentShootingTactics == CurrentShootingTactics.Zone)
            {
                currentShootingTactics = CurrentShootingTactics.Zone;
            }

            if (hitResult == HitResult.Missed && currentShootingTactics == CurrentShootingTactics.Line)
            {
                currentShootingTactics = CurrentShootingTactics.Zone;
            }

        }

        private readonly Grid grid;
        private readonly Fleet fleet;
        private IShootingTactics shootingTactics;

        public CurrentShootingTactics currentShootingTactics { get; private set; }
    }
}
