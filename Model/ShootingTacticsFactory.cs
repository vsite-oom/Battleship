using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class ShootingTacticsFactory
	{
		public ShootingTacticsFactory(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipToShoot)
		{
			this.squaresHit = squaresHit;
			this.evidenceGrid = evidenceGrid;
			this.shipToShoot = shipToShoot;
		}
		public ITargetSelect GetTactics(ShootingTactics tactics)
		{
			switch (tactics)
			{
				case ShootingTactics.Random:
					return new RandomShooting(evidenceGrid, shipToShoot);
				case ShootingTactics.Inline:
					return new InlineShooting(evidenceGrid, squaresHit, shipToShoot);
				case ShootingTactics.Surrounding:
					return new SurroundingShooting(evidenceGrid, squaresHit, shipToShoot);
				default:
					Debug.Assert(false);
					return null;
			}
		}
		private readonly SortedSquares squaresHit;
		private readonly Grid evidenceGrid;
		private readonly List<int> shipToShoot;
	}
}
