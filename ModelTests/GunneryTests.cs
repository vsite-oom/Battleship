using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Vsite.Oom.Battleship.Model.Gunnery;

namespace ModelTests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void CurrentShootingTacticsIsInitiallyRandom()
        {
            var gunnery = new Gunnery(new GameRules());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {
            var gunnery = new Gunnery(new GameRules());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsFromRandomToZoneAfterHit()
        {
            var gunnery = new Gunnery(new GameRules());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.currentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToLineAfterHit()
        {
            var gunnery = new Gunnery(new GameRules());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Line, gunnery.currentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneAfterMiss()
        {
            var gunnery = new Gunnery(new GameRules());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.currentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesToRandomAfterSunk()
        {
            var gunnery = new Gunnery(new GameRules());
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.nextTarget();
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
        }
    }
}