using Model;

namespace ModelTests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void InitialTactics_IsRandom()
        {
            var gunnery = new Gunnery(10, 10, new[] { 5, 4, 3, 3, 2 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }

        [TestMethod]
        public void AfterHit_TacticsChangesToSurrounding()
        {
            var gunnery = new Gunnery(10, 10, new[] { 5, 4, 3, 3, 2 });
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }

        [TestMethod]
        public void AfterSecondHit_TacticsChangesToInline()
        {
            var gunnery = new Gunnery(10, 10, new[] { 5, 4, 3, 3, 2 });
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }

        [TestMethod]
        public void AfterSunken_TacticsResetsToRandom()
        {
            var gunnery = new Gunnery(10, 10, new[] { 5, 4, 3, 3, 2 });
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }

        [TestMethod]
        public void AfterMiss_TacticsStaysSame()
        {
            var gunnery = new Gunnery(10, 10, new[] { 5, 4, 3, 3, 2 });
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }

        [TestMethod]
        public void Next_ReturnsSquare()
        {
            var gunnery = new Gunnery(10, 10, new[] { 3, 2 });
            var square = gunnery.Next();
            Assert.IsNotNull(square);
        }

        [TestMethod]
        public void FullSequence_RandomToSurroundingToInlineToRandom()
        {
            var gunnery = new Gunnery(10, 10, new[] { 3 });

            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);

            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Hit);

            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
            gunnery.Next();
            gunnery.ProcessHitResult(HitResult.Sunken);

            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }
    }
}
