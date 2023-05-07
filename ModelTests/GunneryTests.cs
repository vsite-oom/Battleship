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
            Gunnery gunnery = new(new GameRules());

            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {
            Gunnery gunnery = new(new GameRules());
            gunnery.ProcessHitResult(HitResult.Missed);

            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsChangesFromRandomToZoneAfterHit()
        {
            Gunnery gunnery = new(new GameRules());
            gunnery.ProcessHitResult(HitResult.Hit);

            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToLineAfterHit()
        {
            Gunnery gunnery = new(new GameRules());

            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
            
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneAfterMiss()
        {
            Gunnery gunnery = new(new GameRules());

            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
            
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingChangesToRandomAfterSank()
        {
            var gunnery = new Gunnery(new GameRules());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Sank);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterSank()
        {
            var gunnery = new Gunnery(new GameRules());
            gunnery.ProcessHitResult(HitResult.Sank);
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }

        // remaining shooting tactics
    }
}