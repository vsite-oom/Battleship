using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class RandomShooting : ITargetSelect
	{
		public RandomShooting(Grid evidenceGrid, List<int> shipToShoot)
		{
			this.evidenceGrid = evidenceGrid;
			this.shipToShoot = shipToShoot;
		}
		public Square NextTarget()
		{
			int shipLength = shipToShoot[0];
			var placements = evidenceGrid.GetAvailablePlacments(shipLength);
			var allCandidates = placements.SelectMany(seq => seq);
			int index = random.Next(0, allCandidates.Count());
			return allCandidates.ElementAt(index);
		}

		private Random random = new Random();
		private readonly Grid evidenceGrid;
		private readonly List<int> shipToShoot;
	}
}
