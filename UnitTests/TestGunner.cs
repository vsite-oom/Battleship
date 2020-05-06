using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunner
    {
        [TestMethod]
        public void InitiallyShootingTacticsIsRandomAsLongAsFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner gunner = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

            gunner.NextTarget();
            gunner.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSurroundingAfterFirstSquareIsHit()
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
        public void ShootingTacticsChangesFromSurroundingtoInlineAfterSecondSquareIsHit()
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
        public void ShootingTacticsChangesFromInlinetoRandomAfterShipIsSunk()
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
            gunner.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);

        }
    }
}
