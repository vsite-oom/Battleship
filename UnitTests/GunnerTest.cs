using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class GunnerTest
    {
        [TestClass]
        public class TestGunner
        {
            [TestMethod]
            public void InitialyShootingTacticsIsRandomAsLongAsFirstSquareIsHit()
            {
                int[] shipLengths = new int[] { 1, 2, 3 };
                var gunner = new Gunner(6, 6, shipLengths);
                Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
                gunner.NextTarget();
                gunner.ProcessHitResult(HitResult.Missed);
                Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
            }

            [TestMethod]
            public void InitialyShootingTacticsChangesFromRandomToSurrooundingAfterFirstSquareIsHit()
            {
                int[] shipLengths = new int[] { 1, 2, 3 };
                var gunner = new Gunner(6, 6, shipLengths);
                Assert.AreEqual(ShootingTactics.Random, gunner.ShootingTactics);
                gunner.NextTarget();
                gunner.ProcessHitResult(HitResult.Hit);
                Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);

                gunner.NextTarget();
                gunner.ProcessHitResult(HitResult.Missed);
                Assert.AreEqual(ShootingTactics.Surrounding, gunner.ShootingTactics);
            }
            [TestMethod]
            public void InitialyShootingTacticsChangesFromRandomToSurrooundingAfterSecondSquareIsHit()
            {
                int[] shipLengths = new int[] { 1, 2, 3 };
                var gunner = new Gunner(6, 6, shipLengths);
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
            public void InitialyShootingTacticsChangesFromRandomToInlineToRandomAfterShipIsSunk()
            {
                int[] shipLengths = new int[] { 1, 2, 3 };
                var gunner = new Gunner(6, 6, shipLengths);
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
}
