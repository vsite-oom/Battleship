namespace ModelTests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void CurrentShootingTacticsIsInitiallyRandom()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromRandomToZoneAfterHit()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterSunk()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneAfterMiss()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToRandomAfterSunk()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToLineAfterHit()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Line, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingRemainsLineAfterMiss()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Line, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingRemainsLineAfterHit()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Line, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingChangesToRandomAfterSunk()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.NextTarget();
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }
    }
}