using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Gunner(int rows, int columns, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;

        }
        public Square NextTarget()
        {
            lastTarget = SelectTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            //record on evidence grid
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    //eliminate squares around the ship
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
            //modify shooting tactics
            //if missed-no change
            //if 1st change to sorrounding
            //if 2nd change to inline
            //if sunken change to random
        }
        private Square lastTarget;
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
        private Square SelectRandomly()
        {
            var placements=evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allcandidates=placements.SelectMany(seq=>seq);
            int index=random.Next(0, allcandidates.Count());
            return allcandidates.ElementAt(index);
        }

        private Square SelectInline()
        {
            throw new NotImplementedException();
        }

        private Square SelectFromArround()
        {
            throw new NotImplementedException();
        }

        
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private Random random = new Random();
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
