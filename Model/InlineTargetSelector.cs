using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class InlineTargetSelector : ITargetSelector
    {
        public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square>squaresHit, int shipLength)
        {
             this.grid=grid;
             this.squaresHit=squaresHit;
            this.shipLength=shipLength;
        }
        private readonly ShotsGrid grid;
        private readonly IEnumerable<Square> squaresHit;
        private readonly int shipLength;
        private readonly Random = new Random();
        public Square Next()
        {
            var sorted = squaresHit.OrderBy(sq=>sq.Row+sq.Column);
            var directionCandidates= new List<IEnumerable<Square>>();
            //horizontal
            if(sorted.First().Row==sorted.Last().Row)
            {
                var left=grid.GetSquaresInDirection(sorted.First().Row,sorted.First().Column, Direction.Leftwards);
                if (left.Any())
                {
                    directionCandidates.Add(left);
                }
                var right=grid.GetSquaresInDirection(sorted.Last().Row,sorted.Last().Column, Direction.Rightwards);
                if (right.Any())
                {
                    directionCandidates.Add(right);
                }
            }
            //vertical
            else
            {
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
            }
           var groupedByLength = directionCandidates.GroupBy(l=>l.Count());
            var sortedByLength = groupedByLength.OrderByDescending(g=>g.Key);
            var longestDirections=sortedByLength.First();
            var candidates = longestDirections.Count();
            if (candidates==1)
            {
                return longestDirections.First().First();
            }
            int selectedDirectionIndex = random.Next(candidates);
            return longestDirections.ElementAt(selectedIndex).First();
            


        }
    }
}
