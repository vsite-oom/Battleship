using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Surrounding,
        Linear
    }

    public class Gunnery
    {
        public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            var sorted = shipLengths.OrderByDescending(sl => sl);
            shipsToShoot = sorted.ToList(); // TODO: bitno za zavrsni !! OrderByDescending
            targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
        }

        public Square NextTarget()
        {

            lastTarget = targetSelect.NextTarget();
            return lastTarget;
        }

        public void RecordShootingResult(HitResult result)
        {
            evidenceGrid.RecordResult(lastTarget, result);
            if (result == HitResult.Missed)
                return;
            lastHits.Add(lastTarget);
            if (result == HitResult.Sunken)
            {
                var eliminator = new SurroundingSquareEliminator(evidenceGrid.Rows, evidenceGrid.Columns);
                var toEliminate = eliminator.ToEliminate(lastHits);
                foreach (var square in toEliminate)
                    evidenceGrid.RecordResult(square, HitResult.Missed);
                foreach (var square in lastHits)
                    evidenceGrid.RecordResult(square, HitResult.Sunken);
            }
            ChangeTactics(result);
        }

        private void ChangeTactics(HitResult result)
        {
            switch (result)
            {
                case HitResult.Hit:
                    switch (shootingTactics)
                    {
                        case ShootingTactics.Random:
                            shootingTactics = ShootingTactics.Surrounding;
                            Debug.Assert(lastHits.Count == 1);
                            targetSelect = new SurroundingShooting(evidenceGrid, lastHits[0], shipsToShoot[0]);
                            return;
                        case ShootingTactics.Surrounding:
                            shootingTactics = ShootingTactics.Linear;
                            targetSelect = new LinearShooting(evidenceGrid, lastHits, shipsToShoot[0]);
                            return;
                        case ShootingTactics.Linear:
                            return;
                    }
                    break;
                case HitResult.Sunken:
                    shootingTactics = ShootingTactics.Random;
                    int sunkenShipLength = lastHits.Count;
                    shipsToShoot.Remove(sunkenShipLength);
                    lastHits.Clear();
                    targetSelect = new RandomShooting(evidenceGrid, shipsToShoot[0]);
                    return;
            }
        }

        public ShootingTactics ShootingTactics
        {
            get { return shootingTactics; }
        }
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        private Square lastTarget;
        private List<Square> lastHits = new List<Square>();
        private ITargetSelect targetSelect;
        private ShootingTactics shootingTactics = ShootingTactics.Random;
    }
}
