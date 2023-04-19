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
            Gunnery gunnery = new(new GameRules(), new Fleet());

            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {
            Gunnery gunnery = new(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Missed);

            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsChangesFromRandomToZoneAfterHit()
        {
            Gunnery gunnery = new(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);

            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneToLineAfterHit()
        {
            Gunnery gunnery = new(new GameRules(), new Fleet());

            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
            
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }
        
        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneAfterMiss()
        {
            Gunnery gunnery = new(new GameRules(), new Fleet());

            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
            
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTactics.Zone, gunnery.CurrentShootingTactics);
        }

        // remaining shooting tactics
    }
}