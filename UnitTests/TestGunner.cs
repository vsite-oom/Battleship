using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.BattleShip.Model.UnitTests
{
    [TestClass]
    public class TestGunner
    {
        [TestMethod]
        public void InitialShootingTacticIsRandomAsLongAsFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner gunner = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticChangesFromRandomToSurroundingAfterFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner gunner = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticChangesFromSurroundingToInlineAfterSecondSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner gunner = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunner.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticChangesFromInlineToRandomAfterShipIsSunk()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner gunner = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
        }
    }
}
