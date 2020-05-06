using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunner
    {
        private Square lastTarget;
        private readonly Grid evidenceGrid;
        private List<int> shipsToShoot;
        private readonly Random random = new Random();

        public Gunner(int rows, int columns, IEnumerable<int> shipLenghts)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = shipLenghts.OrderByDescending(s => s).ToList();
            ShootingTactics = ShootingTactics.Random;
        }

        public ShootingTactics ShootingTactics { get; private set; }

        public Square NextTarget()
        {
            return SelectTarget();
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
        }

        private Square SelectTarget()
        {
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    return SelectRandomly();
                case ShootingTactics.Surrounding:
                    return SelectFromAround();
                case ShootingTactics.Inline:
                    return SelectInline();
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        private Square SelectInline()
        {
            throw new NotImplementedException();
        }

        private Square SelectFromAround()
        {
            throw new NotImplementedException();
        }

        private Square SelectRandomly()
        {
            var allCandidates = evidenceGrid.GetAvailablePlacements(shipsToShoot.First()).SelectMany(s => s).ToList();
            var index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }
    }
}
