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
            squareTerminator = new SquareTerminator(rows, columns);
        }

        public Square NextTarget()
        {
            // TODO: Implement correctly!
            // return lastTarget;
            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            // record on evidence grid
            evidenceGrid.MarkHitResult(lastTarget, hitResult);

            // modify shooting tactics
            switch (hitResult)
            {
                case HitResult.Missed:
                    return;
                case HitResult.Sunken:
                    // eliminate squares around the ship
                    squaresHit.Add(lastTarget);
                    squaresHit.OrderBy(s => s.Row + s.Column);
                    var toEliminate = squareTerminator.ToEliminate(squaresHit);
                    foreach(var sq in toEliminate)
                        evidenceGrid.MarkHitResult(sq,HitResult.Missed);
                    foreach (var sq in squaresHit)
                        evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                    int length = squaresHit.Count();
                    shipsToShoot.Remove(length);
                    squaresHit.Clear();
                    ShootingTactics = ShootingTactics.Random;
                    return;
                case HitResult.Hit:
                    squaresHit.Add(lastTarget);
                    squaresHit.OrderBy(s => s.Row + s.Column);
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
            
            // if missed - no change
            // if first - change to surrounding
            // if second hit - change to inline
            // if sunken - change to random
        }

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
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allCandidates = placements.SelectMany(seq => seq);
            int index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }

        private Square SelectInline()
        {
            throw new NotImplementedException();
        }

        private Square SelectFromArround()
        {
            throw new NotImplementedException();
        }

       
        private Square lastTarget;

        private Grid evidenceGrid;

        private List<int> shipsToShoot;

        private List<Square> squaresHit = new List<Square>();

        public Random random = new Random();


        private ISquareTerminator squareTerminator;
        public ShootingTactics ShootingTactics { get; private set; }
    }
}
