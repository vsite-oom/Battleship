using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class RandomShooting : ITargetSelect
	{

		Random random = new Random();
		readonly Grid evidenceGrid;
		private readonly List<int> shipsToShoot;

		public RandomShooting(Grid evidenceGrid, List<int> shipsToShoot)
		{
			this.shipsToShoot = shipsToShoot;
			this.evidenceGrid = evidenceGrid;
		}

		public Square NextTarget()
		{
			int shipLenght = shipsToShoot[0];

			var placements = evidenceGrid.GetAvailablePlacements(shipLenght);
			var allCandidates = placements.SelectMany(seq => seq);
			var groups = allCandidates.GroupBy(sq => sq);
			var maxCount = groups.Max(g => g.Count());
			var lagrestGroup = groups.Where(g => g.Count() == maxCount);
			var mostCommon = lagrestGroup.Select(g => g.Key);

			if (mostCommon.Count() == 1)
				return mostCommon.First();

			int index = random.Next(0, allCandidates.Count());
			return allCandidates.ElementAt(index);
		}
	}
}
