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
            
            
            var gunnery=new Gunnery(new GameRules(), new Fleet());
            Assert.AreEqual(CurrentShootingTacttics.Random, gunnery.CurrentShootingTacttics);
        }


        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterSunkShot()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTacttics.Random, gunnery.CurrentShootingTacttics);

        }
        [TestMethod]
        public void CurrentShootingTacticsRemainsRandomAfterMissedShot()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTacttics.Random, gunnery.CurrentShootingTacttics);

        }
        [TestMethod]
        public void CurrentShootingTacticsChangesFromRandomTacticToZoneAfterHit()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTacttics.Zone, gunnery.CurrentShootingTacttics);

        }
        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneTacticToLineAfterHit()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTacttics.Line, gunnery.CurrentShootingTacttics);

        }
        [TestMethod]
        public void CurrentShootingTacticsRemainsInZoneTacticAfterMiss()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTacttics.Zone, gunnery.CurrentShootingTacttics);

        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromZoneTacticToRandomAfterSunk() 
        { 


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Sunk);
            Assert.AreEqual(CurrentShootingTacttics.Random, gunnery.CurrentShootingTacttics);

        }

        [TestMethod]
        public void CurrentShootingTacticsChangesFromLinerTacticToRandomAfterSunk()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Sunk);

            Assert.AreEqual(CurrentShootingTacttics.Random, gunnery.CurrentShootingTacttics);

        }

        public void CurrentShootingTacticsRemainsInLinearTacticAfterHit()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(CurrentShootingTacttics.Line, gunnery.CurrentShootingTacttics);

        }
        public void CurrentShootingTacticsRemainsInLinearTacticAfterMissed()
        {


            var gunnery = new Gunnery(new GameRules(), new Fleet());
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Hit);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(CurrentShootingTacttics.Line, gunnery.CurrentShootingTacttics);

        }



    }
}