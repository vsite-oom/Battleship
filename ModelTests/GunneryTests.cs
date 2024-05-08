using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void InitionalShootingTacticsIsRandom()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainsRandomIfHits()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainsRandomIfHits2()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainsRandomIfHits1()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsChangesFromSurroundingToInline()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainsInlineAfterThirdSquareIsMissed()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsChangesToRandomAfterShipIsSunken()
        {
            var gunnery = new Gunnery(10,10,new List<int> { 1,2,3});
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }


    }
}