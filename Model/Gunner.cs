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
            squareTerminator = new squareTerminator(rows, columns);
            ShootingTacticsFactory = new ShootingTacticsFactory(evidenceGrid, SquaresHit, shipsToShoot);
            targetSelect = ShootingTacticsFactory.GetTactics(ShootingTactics.Random);
        }
        public Square NextTarget()
        {
            lastTarget = targetSelect.NextTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            if (hitResult == HitResult.Missed)
                return;
            SquaresHit.Add(lastTarget);
            if (hitResult == HitResult.Sunken)
            {
                var toElim = squareTerminator.ToEliminate(SquaresHit);
                foreach (var sq in toElim)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                }
                foreach (var sq in SquaresHit)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                }
                int length = SquaresHit.Length;
                shipsToShoot.Remove(length);
                SquaresHit.Clear();
            }
            ChangeTactics(hitResult);
        }

        private void ChangeTactics(HitResult hitResult)
        {
            if (hitResult == HitResult.Sunken)
            {
                ShootingTactics = ShootingTactics.Random;
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
                        break;
                    case ShootingTactics.Inline:
                        return;
                }
            }
            targetSelect = ShootingTacticsFactory.GetTactics(ShootingTactics);
        }

        private Square lastTarget;
        private Grid evidenceGrid;
        private List<int> shipsToShoot;
        public ShootingTactics ShootingTactics { get; private set; }
        private Random random = new Random();
        private SortedSquares SquaresHit = new SortedSquares();
        private ISquareTerminator squareTerminator;
        private ITargetSelect targetSelect;
        private ShootingTacticsFactory ShootingTacticsFactory;
    }
}
