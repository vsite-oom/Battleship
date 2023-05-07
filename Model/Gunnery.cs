using System;
using System.Collections.Generic;
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
        private readonly Fleet fleet;
        private readonly IShootingTactics shootingTactics;

        public Gunnery(GameRules rules, Fleet fleet)
        {
            grid = new(rules.gridRows, rules.gridColumns);
            this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
            CurrentShootingTactics = CurrentShootingTactics.Random;
        }

        public Square NextTarget()
        {
            return shootingTactics.AddNextTarget();
        }

        public void ProcessHitResult(HitResult hitResult)
        {
        }

        public CurrentShootingTactics CurrentShootingTactics { get; private set; }
    }
}
