using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunner
    {
        private readonly Random random = new Random();
        private readonly Grid evidenceGrid;
        private readonly ISquareTerminator squareTerminator;
        private Square lastTarget;
        private List<int> shipsToShoot;
        private List<Square> squaresHit = new List<Square>();

        public Gunner(int rows, int columns, IEnumerable<int> shipLenghts)
        {
            evidenceGrid = new Grid(rows, columns);
            shipsToShoot = shipLenghts.OrderByDescending(s => s).ToList();
            ShootingTactics = ShootingTactics.Random;
            this.squareTerminator = new SquareTerminator(rows, columns);
        }

        public ShootingTactics ShootingTactics { get; private set; }

        public Square NextTarget()
        {
            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcessHitResult(ShipHitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            switch (hitResult)
            {
                case ShipHitResult.Missed:
                    return;
                case ShipHitResult.Hit:
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
                        default:
                            return;
                    }
                case ShipHitResult.Sunken:
                    squaresHit.Add(lastTarget);
                    squaresHit.OrderBy(s => s.Row + s.Column);
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
                    ShootingTactics = ShootingTactics.Random;
                    return;
                default:
                    return;
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
            throw new NotImplementedException();
        }

        private Square SelectFromAround()
        {
            throw new NotImplementedException();
        }

        private Square SelectRandomly()
        {
            var allCandidates = evidenceGrid.GetAvailablePlacements(shipsToShoot.First()).SelectMany(s => s).ToList();
            var index = random.Next(0, allCandidates.Count());
            return allCandidates.ElementAt(index);
        }
    }
}
