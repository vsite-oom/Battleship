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
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSorroundingAfterFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Sorrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Sorrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToLineAfterFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Sorrounding, g.ShootingTactics);

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
        public void ShootingTacticsChangesFromInlineToRandomAfterShipIsSunk()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Sorrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }
    }
}
