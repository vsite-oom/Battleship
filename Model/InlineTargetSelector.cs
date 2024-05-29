using System;
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
            this.squaresHit = squaresHit;
            this.shipLength = shipLength;
           
        }

        private readonly ShotsGrid grid;
        private readonly List<Square> squaresHit;
        private readonly int shipLength;
        private readonly Random random = new Random();

        public Square Next()
        {
            var sorted = squaresHit.OrderBy(square => square.Row + square.Column);
            var directionCandidates = new List<IEnumerable<Square>>();
            
            //Horizontalno
            if (sorted.First().Row == sorted.Last().Row)
            {
                var left = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Leftwards);
                if(left.Any())
                {
                    directionCandidates.Add(left);
                }

                var right = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Rightwards);
                if (right.Any())
                {
                    directionCandidates.Add(right);
                }

            }
            //Vertikalno
            else
            {

                var left = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
                if (left.Any())
                {
                    directionCandidates.Add(up);
                }

                var right = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Downwards);
                if (right.Any())
                {
                    directionCandidates.Add(down);
                }

            }


            var groupByLength = directionCandidates.GroupBy(l => l.Count());
            var sortedByLength = groupByLength.OrderByDescending(g => g.Key);
            var longestDirections = sortedByLength.First();
            var candidates = longestDirections.Count();
            if (candidates == 1)
            {
                return longestDirections.First().First();
            }
            int selectedIndex = random.Next(candidates);
            return longestDirections.ElementAt(selectedIndex).First();

        }

        public void ProcessHitResult(HitResult hitResult)
        {
            throw new NotImplementedException();
        }
    }
}
