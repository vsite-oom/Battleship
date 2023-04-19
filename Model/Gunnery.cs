using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunnery
    {
        public Gunnery(GameRules gameRules, Fleet fleet)
        {
            grid = new Grid (gameRules.GridRows, gameRules.GridColumns);
            this.fleet = fleet;
            shootingTactics = new RandomShooting(grid);
       
        }

        public Square NextTarget()
        {
            return shootingTactics.NextTarget();
        }

        private readonly Grid grid;
        private readonly Fleet fleet;
        private IShootingTactics shootingTactics;
    }
}
