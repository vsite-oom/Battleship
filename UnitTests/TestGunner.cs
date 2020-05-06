using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunner
    {
        [TestMethod]
        public void InitiallyShootingTacticsIsRandomAsLongAsFirstSquareIsHit()
        {
            var shipLenghts = new int[] { 1, 2, 3 };
            var g = new Gunner(6, 6, shipLenghts);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangeFromRandomToSurroundingAsFirstSquareIsHit()
        {
            var shipLenghts = new int[] { 1, 2, 3 };
            var g = new Gunner(6, 6, shipLenghts);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangeFromSurroundingToInlineAsSecondSquareIsHit()
        {
            var shipLenghts = new int[] { 1, 2, 3 };
            var g = new Gunner(6, 6, shipLenghts);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangeFroInlineToRandomAsLastSquareIsHit()
        {
            var shipLenghts = new int[] { 1, 2, 3 };
            var g = new Gunner(6, 6, shipLenghts);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, g.ShootingTactics);

            g.NextTarget();
            g.ProcessHitResult(ShipHitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }
    }
}
