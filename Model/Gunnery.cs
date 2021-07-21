using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model {
    public enum ShootingTactics {
        Random, Surrounding, Linear
    }

    public class Gunnery {
        public ShootingTactics ShootingTactics { get { return shootingTactics; } }
        private readonly Grid EvidenceGrid;
        private readonly List<int> ShipsToShoot;
        private readonly List<Square> lastHits = new List<Square>();
        private Square lastTarget;
        private ITargetSelect TargetSelect;
        private ShootingTactics shootingTactics = ShootingTactics.Random;

        public Gunnery(int rows, int columns, IEnumerable<int> shipLenghts) {
            var Sorted = shipLenghts.OrderByDescending(sl => sl).ToArray();

            EvidenceGrid = new Grid(rows, columns);
            ShipsToShoot = Sorted.ToList();
            TargetSelect = new RandomShooting(EvidenceGrid, ShipsToShoot);
        }

        public Square NextTarget() {
            lastTarget = TargetSelect.NextTarget();
            return lastTarget;
        }

        public void RecordShootingResult(HitResult result) {
            EvidenceGrid.RecordResult(lastTarget, result);

            if (result == HitResult.Missed) {
                return;
            }

            lastHits.Add(lastTarget);

            if (result == HitResult.Sunken) {
                // mark all squares around lastHits as missed
                var eliminator = new SurroundingSquaresEliminator(EvidenceGrid.rows, EvidenceGrid.colums);
                eliminator.ToEliminate(lastHits);

                // mark all squares in lastHits
                foreach (Square square in lastHits) {
                    square.SetSquareState(HitResult.Sunken);
                }
            }
            ChangeTactics(result);
        }

        private void ChangeTactics(HitResult result) {
            switch (result) {
                case HitResult.Missed:
                    return;

                case HitResult.Hit:
                    switch (shootingTactics) {
                        case ShootingTactics.Random:
                            shootingTactics = ShootingTactics.Surrounding;
                            Debug.Assert(lastHits.Count == 1);
                            TargetSelect = new SurroundingShooting(EvidenceGrid, lastHits[0], ShipsToShoot[0]);
                            return;

                        case ShootingTactics.Surrounding:
                            shootingTactics = ShootingTactics.Linear;
                            TargetSelect = new LinearShooting(EvidenceGrid, lastHits, ShipsToShoot[0]);
                            return;

                        case ShootingTactics.Linear:
                            return;

                        default:
                            break;
                    }
                    break;

                case HitResult.Sunken:
                    shootingTactics = ShootingTactics.Random;
                    int sunkenShipLength = lastHits.Count;
                    ShipsToShoot.Remove(sunkenShipLength);
                    lastHits.Clear();
                    TargetSelect = new RandomShooting(EvidenceGrid, ShipsToShoot);
                    return;

                default:
                    break;
            }
        }
    }
}