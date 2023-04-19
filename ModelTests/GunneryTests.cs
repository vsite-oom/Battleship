using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void CurrentShootingTacticsIsInitiallyRandom()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules, new Fleet());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules, new Fleet());
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromRandomToZoneAfterHit()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules, new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToLineAfterHit()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules, new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Line, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneAfterMiss()
        {
            var gameRules = new GameRules();
            var gunnery = new Gunnery(gameRules, new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }
    }
}