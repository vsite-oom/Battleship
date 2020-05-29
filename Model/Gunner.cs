using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunner
    {
        private readonly Grid evidenceGrid;
        private readonly ISquareTerminator squareTerminator;
        private Square lastTarget;
        private List<int> shipsToShoot;
        private SortedSquares squaresHit = new SortedSquares();
        private ITargetSelect targetSelect;

        public Gunner()
        {
            evidenceGrid = new Grid(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns);
            shipsToShoot = RulesSingleton.Instance.ShipLengths.OrderByDescending(s => s).ToList();
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns);
            targetSelect = new RandomShooting(evidenceGrid);
        }

        public Gunner(int rows, int columns, IEnumerable<int> shipLenghts)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = shipLenghts.OrderByDescending(s => s).ToList();
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);
            targetSelect = new RandomShooting(evidenceGrid);
        }

        public ShootingTactics ShootingTactics { get; private set; }

        public Square NextTarget()
        {
            lastTarget = targetSelect.NextTarget(shipsToShoot[0]);
            return lastTarget;
        }
        
        public void ProcessHitResult(ShipHitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            if (hitResult == ShipHitResult.Missed)
            {
                return;
            }
            squaresHit.Add(lastTarget);

            if (hitResult == ShipHitResult.Sunken)
            {
                squaresHit.Add(lastTarget);
                var toEliminate = squareTerminator.ToEliminate(squaresHit);
                foreach (var sq in toEliminate)
                {
                    evidenceGrid.MarkHitResult(sq, ShipHitResult.Missed);
                }

                foreach (var sq in squaresHit)
                {
                    evidenceGrid.MarkHitResult(sq, ShipHitResult.Sunken);
                }

                var length = squaresHit.Count;
                shipsToShoot.Remove(length);
                squaresHit.Clear();
            }

            ChangeTactics(hitResult);
        }

        private void ChangeTactics(ShipHitResult hitResult)
        {
            if (hitResult == ShipHitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
                targetSelect = new RandomShooting(evidenceGrid);
                return;
            }

            if (hitResult == ShipHitResult.Hit)
            {
                switch (ShootingTactics)
                {
                    case ShootingTactics.Random:
                        ShootingTactics = ShootingTactics.Surrounding;
                        targetSelect = new SurroundShooting(evidenceGrid, squaresHit);
                        return;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        targetSelect = new InlineShooting(evidenceGrid, squaresHit);
                        return;
                    case ShootingTactics.Inline:
                        return;
                    default:
                        return;
                }
            }
        }
    }
}
