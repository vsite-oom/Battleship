using System.Collections.Generic;
using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public class ShootingTacticsFactory
    {
        private readonly Grid evidenceGrid;
        private readonly SortedSquares squaresHit;
        private readonly List<int> shipsToShoot;

        public ShootingTacticsFactory(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
        {
            this.evidenceGrid = evidenceGrid;
            this.squaresHit = squaresHit;
            this.shipsToShoot = shipsToShoot;
        }

        public ITargetSelect GetTactics(ShootingTactics tactics)
        {
            switch (tactics)
            {
                case ShootingTactics.Random:
                    return new RandomShooting(evidenceGrid, shipsToShoot);
                case ShootingTactics.Surrounding:
                    return new SurroundShooting(evidenceGrid, squaresHit, shipsToShoot);
                case ShootingTactics.Inline:
                    return new InlineShooting(evidenceGrid, squaresHit, shipsToShoot);
                default:
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}