using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests {
    [TestClass]
    public class TestGunnery {
        [TestMethod]
        public void InitiallyShootingTacticsIsRandom() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainsRandomForSquareMissed() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRandomToSurroundingWhenFirstSquaresIsHit() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainSurroindingAfterSquareIsMissed() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
            g.RecordShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesFromRSurrundingToLinearAfterSecondSquareIsHit() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Hit);
            g.RecordShootingResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Linear, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsRemainsLinearAfterSquareIsMissed() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Hit);
            g.RecordShootingResult(HitResult.Hit);
            g.RecordShootingResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Linear, g.ShootingTactics);
        }

        [TestMethod]
        public void ShootingTacticsChangesToRandomAfterShipIsSunken() {
            Gunnery g = new Gunnery(10, 10, new List<int> { 5, 3 });
            g.RecordShootingResult(HitResult.Hit);
            g.RecordShootingResult(HitResult.Hit);
            g.RecordShootingResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, g.ShootingTactics);
        }
    }
}