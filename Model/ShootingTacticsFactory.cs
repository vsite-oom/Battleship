using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ShootingTacticsFactory
    {

        public ShootingTacticsFactory(SortedSquares squaresHit, Grid evidenceGrid, List<int> shipsToShoot)
        {
            this.squaresHit = squaresHit;
            this.evidenceGrid = evidenceGrid;
            this.shipsToShoot = shipsToShoot;
        }

        public ITargetSelect GetTactics(ShootingTactics tactics)
        {
            switch (tactics)
            {
                case ShootingTactics.Random:
                    return new RandomShooting(evidenceGrid, shipsToShoot);
                case ShootingTactics.Surrounding:
                    return new SurroundShooting(squaresHit, evidenceGrid, shipsToShoot);
                case ShootingTactics.Inline:
                    return new InlineShooting(squaresHit, evidenceGrid, shipsToShoot);
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        private readonly SortedSquares squaresHit;
        private readonly Grid evidenceGrid;
        private List<int> shipsToShoot;
    }
}
