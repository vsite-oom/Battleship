using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum ShootingTactics
    {
        Random,
        Sorrounding,
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
            //TODO: implement correctly
            lastTarget = new Square(0, 0);

            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            /*record on evidence grid
            */
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    //eliminate squares arround the ship
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
                    switch(ShootingTactics)                    
                    {
                        case ShootingTactics.Random:
                            ShootingTactics = ShootingTactics.Sorrounding;
                            return;
                        case ShootingTactics.Sorrounding:
                            ShootingTactics = ShootingTactics.Inline;
                            return;
                        case ShootingTactics.Inline:
                            return;
                    }
                    break;
            }
            /* 
              modify shooting tactics
                -if missed - no change
                -if first - change to surrounding
                -if second hit - change to inline
                -if sunken - change to random
            */
        }

        private Square lastTarget;

        private Grid evidenceGrid;

        private List<int> shipsToShoot;

        public  ShootingTactics ShootingTactics { get; private set; }
    }
}
