using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Vsite.Oom.Battleship.Model.Ship;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Inline
    }
    public class Gunner
    {
        public Gunner(int rows, int colums, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, colums);
            shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
        }
        public Square NextTarget()
        {
            //TODO: implement correctly
            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcesHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            // record on evidence grid
            // modify shooting tactics
            // - if missed no change
            // - if first hit change to line shooting
            // - if second hit change to inline
            // - if sunk change to random
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunk:
                    // eliminate squares around the ship
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
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

                    }
                    break;
            }

        }

        private Square SelectTarget()
        {
            switch (ShootingTactics)
            {
                case ShootingTactics.Random:
                    return SelectRandomly();
                case ShootingTactics.Surrounding:
                    return SelectFromArround();
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

        private Square SelectFromArround()
        {
            throw new NotImplementedException();
        }

        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]).SelectMany(s=> s);           
            var index = random.Next(0, placements.Count());
            return placements.ElementAt(index);
            

        }
       
        private Random random = new Random();
        private Square lastTarget;
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
