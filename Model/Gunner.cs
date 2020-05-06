using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
           lastTarget =  SelectTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            switch(hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    // eliminate squares around the ship
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
                    switch (ShootingTactics) {
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
            switch(ShootingTactics)
            {
                case ShootingTactics.Random:
                    return SelectRandomly();
                case ShootingTactics.Surrounding:
                    return SelectFromAround();
                case ShootingTactics.Inline:
                    return SelectInLine();
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        private Square SelectInLine()
        {
            throw new NotImplementedException();
        }

        private Square SelectFromAround()
        {
            throw new NotImplementedException();
        }

        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allCandidates = placements.SelectMany(seq => seq);
            int index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }

        private Square lastTarget;

        private Grid evidenceGrid;

        private Random random = new Random();

        private List<int> shipsToShoot;
        public ShootingTactics ShootingTactics { get; private set; }

    }
}
