using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void InitialShootingTacticsIsRandom()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.shootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainRandomOnTargetMiss()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunnery.shootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsChangesToSurrondingOnHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsRemainsOnSurrondingOnMiss()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
        } 
        [TestMethod]
        public void ShootingTacticsChangesToInlineOnHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
        } 
        [TestMethod]
        public void ShootingTacticsRemainsOnInlineOn3rdHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
        } 
        [TestMethod]
        public void ShootingTacticsRemainsOnInlineOnMiss()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.shootingTactics);
        } 
        [TestMethod]
        public void ShootingTacticsChangeToRandomAfterShipSunk()
        {
            var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surronding, gunnery.shootingTactics);
            gunnery.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, gunnery.shootingTactics);

        }

    }
}