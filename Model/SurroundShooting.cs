using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class SurroundShooting : ITargetSelect
	{
		
		Random random = new Random();
		readonly SortedSquares squaresHit = new SortedSquares();
		readonly Grid evidenceGrid;

		public SurroundShooting(Grid evidenceGrid, SortedSquares squaresHit)
		{
			this.squaresHit = squaresHit;
			this.evidenceGrid = evidenceGrid;

		}
		public Square NextTarget(int shipLenght)
		{
			List<IEnumerable<Square>> around = new List<IEnumerable<Square>>();

			foreach (Direction direction in Enum.GetValues(typeof(Direction)))
			{
				var l = evidenceGrid.GetSquaresNextTo(squaresHit.First(), direction);
				if (l.Count() > 0)
					around.Add(l);
			}
			if (around.Count == 1)
				return around[0].First();

			var ordered = around.OrderByDescending(ls => ls.Count());
			int maxLen = ordered.First().Count();
			if (maxLen > shipLenght - 1)
				maxLen = shipLenght - 1;

			var longest = ordered.Where(ls => ls.Count() >= maxLen);

			int index = random.Next(0, longest.Count());
			return longest.ElementAt(index).First();
		}
	}
}
