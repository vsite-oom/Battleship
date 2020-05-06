using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestGunner
    {
        [TestMethod]
        public void InitiallyShootingTacticsIsRandomAsLongAsFirstSquareIsHit()
        {
            int[] shipLengths = new int[] { 1, 2, 3};
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSurroundingAfterSquareIsHIt()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromSurroundingToInlineAfterSecondSquareIsHIt()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);
           
            g.NextTarget();
            g.ProcesHItResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromInlineToRandomAfterSquareIsSunk()
        {
            int[] shipLengths = new int[] { 1, 2, 3 };
            Gunner g = new Gunner(6, 6, shipLengths);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcesHItResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }
    }
}
