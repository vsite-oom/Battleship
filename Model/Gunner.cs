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
            shipsToShoot = new List<int> (shipLengths.OrderByDescending(l => l));
            ShootingTactics = ShootingTactics.Random;
            squareTerminator = new SquareTerminator(rows, columns);
        }

        public Square NextTarget()
        {
            lastTarget = SelectTarget();
            return lastTarget;
        }

        public void ProcessHitResult(HitResult hitResult)
        {
            evidenceGrid.MarkHitResult(lastTarget, hitResult);
            if (hitResult == HitResult.Missed)
            {
                return;
            }
            squaresHit.Add(lastTarget);
            if (hitResult == HitResult.Sunken)
            {
                var toEliminate = squareTerminator.ToEliminate(squaresHit);
                foreach (var sq in toEliminate)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Missed);
                }
                foreach (var sq in squaresHit)
                {
                    evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
                }
                evidenceGrid.EliminateSquares(toEliminate);
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
                        return;
                    case ShootingTactics.Surrounding:
                        ShootingTactics = ShootingTactics.Inline;
                        return;
                    case ShootingTactics.Inline:
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

        private Square SelectRandomly()
        {
            var placements = evidenceGrid.GetAvailablePlacements(shipsToShoot[0]);
            var allCandidates = placements.SelectMany(seq => seq);
            var groups = allCandidates.GroupBy(sq => sq);
            var maxCount = groups.Max(g => g.Count());
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            var mostCommon = largestGroups.Select(g => g.Key);
            if (mostCommon.Count() == 1)
            {
                return mostCommon.First();
            }
            int index = random.Next(0, mostCommon.Count());
            return mostCommon.ElementAt(index);
        }

        private Square SelectInline()
        {
            var l = evidenceGrid.GetSquaresInline(squaresHit);
            if (l.Count() == 1)
            {
                return l.ElementAt(0).First();
            }
            var ordered = l.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipsToShoot[0] - squaresHit.Length)
            {
                maxLen = shipsToShoot[0] - squaresHit.Length;
            }
            var longest = ordered.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());
            return longest.ElementAt(index).First();
        }

        private Square SelectFromAround()
        {
            List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
                if (l.Count() > 0)
                {
                    around.Add(l);
                }
            }
            if (around.Count == 1)
            {
                return around[0].First();
            }
            var ordered = around.OrderByDescending(ls => ls.Count());
            int maxLen = ordered.First().Count();
            if (maxLen > shipsToShoot[0] - 1)
            {
                maxLen = shipsToShoot[0] - 1;
            }
            var longest = ordered.Where(ls => ls.Count() >= maxLen);
            int index = random.Next(0, longest.Count());
            return longest.ElementAt(index).First();
        }

        private Square lastTarget;

        private Grid evidenceGrid;

        private ISquareTerminator squareTerminator;

        private SortedSquares squaresHit = new SortedSquares();

        private List<int> shipsToShoot;

        private Random random = new Random();

        public ShootingTactics ShootingTactics { get; private set; }
    }
}
