using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class InlineTargetSelector : ITargetSelector
	{

		public InlineTargetSelector(ShotsGrid grid, IEnumerable<Square> squareHit, int shipLength)
		{
			this.grid = grid;
			this.squareHit = squareHit;
			this.shipLength = shipLength;

		}
		private readonly ShotsGrid grid;
		private readonly IEnumerable<Square> squareHit;
		private readonly int shipLength;
		private readonly Random random = new Random();
		public Square Next()
		{
			var sorted = squareHit.OrderBy(sq => sq.Row + sq.Column);
			var directionCandidates = new List<IEnumerable<Square>>();
			//horizontal
			if (sorted.First().Row == sorted.Last().Row)
			{
				var left = grid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, ShotsGrid.Direction.Leftwards);
				if (left.Any())
				{
					directionCandidates.Add(left);
				}
				var right = grid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, ShotsGrid.Direction.Rightwards);
				if (right.Any())
				{
					directionCandidates.Add(right);
				}
			}
			//vetical
			else
			{

			}
			var groupByLength = directionCandidates.GroupBy(l => l.Count());
			var sortedByLength = groupByLength.OrderByDescending(g => g.Key);
			var longestDirections = sortedByLength.First();
			var candidates = longestDirections.Count();
			if (candidates == 1) { return longestDirections.First().First(); }
			int selectedIndex=random.Next(candidates);
			return longestDirections.ElementAt(selectedIndex).First();

			throw new NotImplementedException();
		}
	}
}
