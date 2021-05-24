using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunnery
    {
        [TestMethod]
        public void initiallyShootingTacticsIsRandom()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }
        /*
        [TestMethod]
        public void ShootingTacticsRemainsRandomForSquareMissed()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSurroundingWhenFirstSquareIsHit()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainsSurroundingAfterSquareIsMissed()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
            g.RecordShooting(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromSurroundingToLinearAfterSecondSquareIsHit()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Hit);
            g.RecordShooting(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Linear, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainsLinearAfterSquareIsMissed()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Hit);
            g.RecordShooting(HitResult.Hit);
            g.RecordShooting(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Linear, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesToRandomAfterShipIsSunken()
        {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShooting(HitResult.Hit);
            g.RecordShooting(HitResult.Hit);
            g.RecordShooting(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }*/
    }
}
