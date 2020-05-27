using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public class ShootingTacticFactory
	{

		readonly Grid evidenceGrid;
		readonly SortedSquares squaresHit;
		readonly List<int> shipsToShoot;
		public ShootingTacticFactory(Grid evidenceGrid, SortedSquares squaresHit, List<int> shipsToShoot)
		{
			this.squaresHit = squaresHit;
			this.evidenceGrid = evidenceGrid;
			this.shipsToShoot = shipsToShoot;
		}
		public ITargetSelect GetTactics(ShootingTactics tactics)
		{

			switch (tactics)
			{
				case ShootingTactics.Random:
					return new RandomShooting(evidenceGrid, shipsToShoot);
				case ShootingTactics.Surrounding:
					return new SurroundShooting(evidenceGrid, squaresHit, shipsToShoot);
				case ShootingTactics.Inline:
					return new InlineShooting(evidenceGrid, squaresHit, shipsToShoot);
				default:
					Debug.Assert(false);
					return null;
			}
		}
	}
}
