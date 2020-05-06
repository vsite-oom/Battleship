using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunner
    {
        private Square lastTarget;
        private readonly Grid evidenceGrid;
        private List<int> shipsToShoot;

        public Gunner(int rows, int columns, IEnumerable<int> shipLenghts)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = shipLenghts.OrderByDescending(s => s).ToList();
            ShootingTactics = ShootingTactics.Random;
        }

        public ShootingTactics ShootingTactics { get; private set; }

        public Square NextTarget()
        {
            lastTarget = new Square(0, 0);
            return lastTarget;
        }

        public void ProcessHitResult(ShipHitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            switch (hitResult)
            {
                case ShipHitResult.Missed:
                    return;
                case ShipHitResult.Hit:
                    switch (ShootingTactics)
                    {
                        case ShootingTactics.Random:
                            ShootingTactics = ShootingTactics.Surrounding;
                            return;
                        case ShootingTactics.Surrounding:
                            ShootingTactics = ShootingTactics.Inline;
                            return;
                        case ShootingTactics.Inline:
                            return;
                        default:
                            return;
                    }
                case ShipHitResult.Sunken:
                    ShootingTactics = ShootingTactics.Random;
                    return;
                default:
                    return;
            }
            // modify shooting tactics
            // - if missed - no change
            // - if first hit - change to surrounding
            // - if second - change to inline
            // - if sunken - change to random 
        }
    }
}
