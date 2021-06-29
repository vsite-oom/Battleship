using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGunnery
    {
        [TestMethod]
        public void InitialShootingTacticsIsRandom()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);

        }
        [TestMethod]
        public void GunneryDoesntChangeShootingTacticsWhenResultIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int>{ 5, 4, 4 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);
            gunnery.ProcessShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);
        }

        [TestMethod]
        public void GunneryChangesShootingTacticsFromRandomToSurroundingWhenSqaureIsHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);
            gunnery.ProcessShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.CurrentTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainsSurroundingIfSquareIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);
            gunnery.ProcessShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.CurrentTactics);
            gunnery.ProcessShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.CurrentTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromSurroundingToLinearAfterSecondSquareIsHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            gunnery.ProcessShootingResult(HitResult.Hit);
            gunnery.ProcessShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Linear, gunnery.CurrentTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesReamainsLinearIfSquareIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            gunnery.ProcessShootingResult(HitResult.Hit);
            gunnery.ProcessShootingResult(HitResult.Hit);
            gunnery.ProcessShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Linear, gunnery.CurrentTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesToRandomAfterShipIsSunken()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 5, 4, 4 });
            gunnery.ProcessShootingResult(HitResult.Hit);
            gunnery.ProcessShootingResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, gunnery.CurrentTactics);
        }
    }
}
