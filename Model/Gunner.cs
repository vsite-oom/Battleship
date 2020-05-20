using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Vsite.Oom.Battleship.Model
{
    public class Gunner
    {
        private readonly Random random = new Random();
        private readonly Grid evidenceGrid;
        private readonly ISquareTerminator squareTerminator;
        private Square lastTarget;
        private List<int> shipsToShoot;
        private SortedSquares squaresHit = new SortedSquares();

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
                return;
            }

            if (hitResult == ShipHitResult.Hit)
            {
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
            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() > 1)
                l = l.OrderByDescending(ls => ls.Count()).ToList();
            return l.ElementAt(0).First();
        }

        private Square SelectFromAround()
        {
            var arround = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                    arround.Add(l);
            }

            if (arround.Count > 1)
                arround = arround.OrderByDescending(ls => ls.Count()).ToList();
            return arround[0].First();
        }

        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            // create simple array of sqaures from arrays of arrays
            var allCandidates = placements.SelectMany(seq => seq);
            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);
            // find the number of squares in largest group
            var maxCount = groups.Max(g => g.Count());
            // filter only froups that have maxCount elements
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            // fetch keys from each group (i.e. square that represents the group)
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count() == 1)
                return mostCommon.First();
            var index = random.Next(0, mostCommon.Count());
            return mostCommon.ElementAt(index);
        }
    }
}
