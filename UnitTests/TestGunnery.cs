using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunnery
    {
        [TestMethod]
        public void InitiallyShootingTacticsIsRandom()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });
            
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsRemainsRandomForSquareMissed()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });
            gunnery.RecordShootingResult(HitResult.Missed);
            
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSorroundingWhenFirstSquareIsHit()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });
            gunnery.RecordShootingResult(HitResult.Hit);

            Assert.AreEqual(ShootingTactics.Sorrounding, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsRemainsSorroundingAfterSquareIsMissed()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });

            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Missed);

            Assert.AreEqual(ShootingTactics.Sorrounding, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromSorroundingAfterSecondSquareIsHit()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });

            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Missed);
            gunnery.RecordShootingResult(HitResult.Hit);

            Assert.AreEqual(ShootingTactics.Linear, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsRemainsLinearAfterSquareIsMissed()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });

            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Missed);
            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Hit);

            Assert.AreEqual(ShootingTactics.Linear, gunnery.CurrentShootingTactis);
        }

        [TestMethod]
        public void ShootingTacticsChangesToRandomAfterShipIsSunken()
        {
            Gunnery gunnery = new Gunnery(10, 10, new List<int> { 5, 3 });

            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Hit);
            gunnery.RecordShootingResult(HitResult.Sunken);

            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentShootingTactis);
        }
    }
}
