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
        public Gunner(int rows,int columns,IEnumerable<int> shipLengths)
        {
            evidenceGrid = new Grid(rows, columns);
            shipLengths.OrderByDescending(l=>l);
            shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new squareTerminator(rows, columns);
        }
        public Square NextTarget()
        {
            lastTarget = SelectTarget();
            return lastTarget;
        }
        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    SquaresHit.Add(lastTarget);
                    SquaresHit.OrderBy(s => s.row + s.column);
                    var toElim = squareTerminator.ToEliminate(SquaresHit);
                    foreach(var sq in toElim)
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                    }
                    foreach (var sq in SquaresHit)
                    {
                        evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                    }
                    int length = SquaresHit.Count();
                    shipsToShoot.Remove(length);
                    SquaresHit.Clear();
                    ShootingTactics = ShootingTactics.Random;   
                    return;
                case HitResult.Hit:
                    SquaresHit.Add(lastTarget);
                    SquaresHit.OrderBy(s => s.row + s.column);
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
                    return SelectFromAround();
                case ShootingTactics.Inline:
                    return SelectInline();
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        private Square SelectInline()
        {
            var l = evidenceGrid.GetSquaresInline(SquaresHit);
            if (l.Count() == 1)
                return l.ElementAt(0).First();

            var index = random.Next(0, l.Count());
            return l.ElementAt(index).First();
        }

        private Square SelectFromAround()
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(lastTarget, direction);
                if (l.Count() > 0)
                {
                    around.Add(l);
                }
              
            }
            if (around.Count == 1)
            {
                return around[0].First();
            }
            int index = random.Next(0, around.Count());
            return around[index].First();

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
        private List<int> shipsToShoot;
        public ShootingTactics ShootingTactics{get; private set;}
        private Random random = new Random();
        private List<Square> SquaresHit = new List<Square>();
        private ISquareTerminator squareTerminator;
    }
}
