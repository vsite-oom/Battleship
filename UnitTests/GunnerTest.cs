using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
	[TestClass]
	public class GunnerTest
	{
		[TestMethod]
		public void InitiallyShootingTacticsIsRandomAsLongAsFirstSquareIsHit()
		{
			int[] shipLenghts = new int[] { 1, 2, 3 };
			Gunner g = new Gunner(6, 6, shipLenghts);
			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Missed);

			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

		}

		[TestMethod]
		public void InitiallyShootingTacticsChangesFromRandomToSuroundingAfterFirstSquareIsHit()
		{
			int[] shipLenghts = new int[] { 1, 2, 3 };
			Gunner g = new Gunner(6, 6, shipLenghts);
			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Missed);
			Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

		}

		[TestMethod]
		public void InitiallyShootingTacticsChangesFromSuroundingToInlineAfterSecoundSquareIsHit()
		{
			int[] shipLenghts = new int[] { 1, 2, 3 };
			Gunner g = new Gunner(6, 6, shipLenghts);
			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

			g.NextTarget();	
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Missed);
			Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

		}

		[TestMethod]
		public void InitiallyShootingTacticsChangesFromInlineToRandomAfterIsSunk()
		{
			int[] shipLenghts = new int[] { 1, 2, 3 };
			Gunner g = new Gunner(6, 6, shipLenghts);
			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Hit);
			Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

			g.NextTarget();
			g.ProcessHitResult(HitResult.Sunk);
			Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);


		}
	}
}
