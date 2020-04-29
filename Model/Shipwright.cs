using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class Shipwright
	{
		private readonly int rows;
		private readonly int columns;

		private Grid grid;
		public Shipwright()
		{
			rows = RulesSingleton.Instance.Rows;
			columns = RulesSingleton.Instance.Columns;
		}
		public Fleet CreateFleet(IEnumerable<int> shipLengths)
		{

			for (int i = 0; i < 3; i++)
			{
				Fleet fleet = PlaceShips(shipLengths);

				if (fleet != null)
					return fleet;
			}


			throw new ArgumentOutOfRangeException();
		}

		private Fleet PlaceShips(IEnumerable<int> shipLengths)
		{
			List<int> lengths = new List<int>(shipLengths.OrderByDescending(x => x));

			grid = new Grid(rows, columns);
			SquareTerminator terminator = new SquareTerminator(grid);
			Fleet fleet = new Fleet();

			Random random = new Random();

			while (lengths.Count > 0)
			{
				var placemenets = grid.GetAvailablePlacements(lengths[0]);
				if (placemenets.Count() == 0)
					return null;
				lengths.RemoveAt(0);
				int index = random.Next(0, placemenets.Count());
				fleet.AddShip(placemenets.ElementAt(index));

				var toEliminate = terminator.ToEliminate(placemenets.ElementAt(index));
				grid.EliminateSquares(toEliminate);

			}
			return fleet;
		}
	}
}
