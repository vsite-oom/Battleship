using System;
using Battleships_GUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunner
    {
        [TestMethod]
        public void InitallyShootingTacticsIsRandomAsFirstSquareIsHit()
        {
            int[] shipLenghts = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLenghts);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangedFromRandomToSurroundingAsFirstSquareIsHit()
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
        public void ShootingTacticsChangedFromSurroundingToInLineAsFirstSquareIsHit()
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
        public void ShootingTacticsChangedFromInlineToRandomAfterShipIsSunk()
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
            g.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

        }

    }
}
