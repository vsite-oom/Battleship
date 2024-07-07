using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineTargetSelector : ITargetSelector
    {
        public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square> squaresHit, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
            this.squaresHit = squaresHit;
            this.random = new Random();
        }

        private readonly ShotsGrid grid;
        private readonly IEnumerable<Square> squaresHit;
        private readonly int shipLength;
        private readonly Random random;

        public Square Next()
        {
            var sorted = squaresHit.OrderBy(sq => sq.Row + sq.Column).ToList();
            var directionCandidates = new List<IEnumerable<Square>>();

            // Horizontal
            if (sorted.First().Row == sorted.Last().Row)
            {
                var left = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Leftwards);
                if (left.Any())
                {
                    directionCandidates.Add(left);
                }
                var right = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Rightwards);
                if (right.Any())
                {
                    directionCandidates.Add(right);
                }
            }
            // Vertical
            else
            {
                var up = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
                if (up.Any())
                {
                    directionCandidates.Add(up);
                }
                var down = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Downwards);
                if (down.Any())
                {
                    directionCandidates.Add(down);
                }
            }

            if (!directionCandidates.Any())
            {
                // Default to random if no direction candidates are found
                return new RandomTargetSelector(grid, shipLength).Next();
            }

            var groupedByLength = directionCandidates.GroupBy(l => l.Count());
            var sortedByLength = groupedByLength.OrderByDescending(g => g.Key);
            var longestDirections = sortedByLength.FirstOrDefault();

            if (longestDirections == null || !longestDirections.Any())
            {
                // Default to random if no valid directions are found
                return new RandomTargetSelector(grid, shipLength).Next();
            }

            var candidates = longestDirections.ToList();
            if (candidates.Count == 1)
            {
                return candidates.First().First();
            }

            int selectedIndex = random.Next(candidates.Count);
            return candidates.ElementAt(selectedIndex).First();
        }
    }
}
