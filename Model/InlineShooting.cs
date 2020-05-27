using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class InlineShooting : ITargetSelect
	{

		Random random = new Random();
		readonly SortedSquares squaresHit = new SortedSquares();
		readonly Grid evidenceGrid;
		private readonly List<int> shipsToShoot;

		public InlineShooting(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
		{
			this.squaresHit = squaresHit;
			this.evidenceGrid = evidenceGrid;
			this.shipsToShoot = shipsToShoot;
		}
		public Square NextTarget()
		{
			int shipLenght = shipsToShoot[0];
			var l = evidenceGrid.GetSquaresInLine(squaresHit);
			if (l.Count() == 1)
				return l.ElementAt(0).First();

			var ordered = l.OrderByDescending(ls => ls.Count());
			int maxLen = ordered.First().Count();

			if (maxLen > shipLenght - squaresHit.Length)
				maxLen = shipLenght - squaresHit.Length;

			var longest = ordered.Where(ls => ls.Count() >= maxLen);

			int index = random.Next(0, longest.Count());
			return longest.ElementAt(index).First();
		}
	}
}
