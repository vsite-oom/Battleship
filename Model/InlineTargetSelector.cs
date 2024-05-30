﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineTargetSelector : ITargetSelector
    {
        public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square> squaresHit, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
            this.squaresHit = squaresHit;
        }

        private readonly ShotsGrid grid;
        private readonly IEnumerable<Square> squaresHit;
        private readonly int shipLength;
        private readonly Random random;

        public Square Next()
        {
            var sorted = squaresHit.OrderBy(sq => sq.Row + sq.Column);
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
            var groupedByLength = directionCandidates.GroupBy(l => l.Count());
            var sortedByLength = groupedByLength.OrderByDescending(g => g.Key);
            var longestDIrections = sortedByLength.First();
            var candidates = longestDIrections.Count();
            if (candidates == 1)
            {
                return longestDIrections.First().First();
            }
            int selectedIndex = random.Next();
            return longestDIrections.ElementAt(selectedIndex).First();
        }
    }
}
