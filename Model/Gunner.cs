using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Vsite.Oom.Battleship.Model;

namespace Vsite.oom.Battleship.Model
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
            squareTerminator = new SquareTerminator(rows, columns);
            shootingTacticsFactory = new ShootingTacticsFactory(evidenceGrid, squaresHit, shipsToShoot);
            targetSelect = shootingTacticsFactory.GetTactics(ShootingTactics.Random);
        }

        public Square NextTarget()
        {
            // TODO: implement correctly!
            lastTarget = targetSelect.NextTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);

            if (hitResult == HitResult.Missed)
                return;

            squaresHit.Add(lastTarget);

            if (hitResult == HitResult.Sunken)
            {
                var toEliminate = squareTerminator.ToEliminate(squaresHit);

                foreach (var sq in toEliminate)
                    evidenceGrid.MarkHitResult(sq, HitResult.Missed);

                foreach (var sq in squaresHit)
                    evidenceGrid.MarkHitResult(sq, HitResult.Sunken);

                int length = squaresHit.Length;
                shipsToShoot.Remove(length);
                squaresHit.Clear();
            }
            ChangeTactics(hitResult);
        }

        private void ChangeTactics(HitResult hitResult)
        {
            if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;

                return;
            }
            if (hitResult == HitResult.Hit)


            {
                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ShootingTactics = ShootingTactics.Surrounding;
                        break;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        return;
                    
                }
            }
            targetSelect = shootingTacticsFactory.GetTactics(ShootingTactics);
        }

        

        private Square lastTarget;
        private ITargetSelect targetSelect;
        private Grid evidenceGrid;
        private SortedSquares squaresHit = new SortedSquares();
        private List<int> shipsToShoot;
        private Random random = new Random();
        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }
        private ShootingTacticsFactory shootingTacticsFactory;
    }
}

