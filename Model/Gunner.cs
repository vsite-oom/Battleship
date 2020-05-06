using System;
using System.Collections.Generic;
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
            lastTarget =  new Square(0, 0);
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

        private Square lastTarget;
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
