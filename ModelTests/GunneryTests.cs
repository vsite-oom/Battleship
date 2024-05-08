﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vsite.oom.battleship.model;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void InitialShootingTacticsIsRandom()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void InitialShootingTacticsRemainsRandomIfHitResultIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }
        [TestMethod]
        public void ShootingTacticsChangesToSurroundingAfterFirstSquareIsHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }
                [TestMethod]
        public void ShootingTacticsRemainsSurroundingIfNextSquareIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        }
        public void ShootingTacticsChangesFromSurroundingToInlineAfterSecondSquareIsHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }
        public void ShootingTacticsRemainsInlineAfterThirdSquareIsHit()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }
        public void ShootingTacticsRemainsInlineAfterThirdSquareIsMissed()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Missed);
            Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        }
        public void ShootingTacticsChangesToRandomAfterShipIsSunken()
        {
            var gunnery = new Gunnery(10, 10, new List<int> {1, 2, 3 });
            gunnery.ProcessHitResult(HitResult.Hit);
            Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
            gunnery.ProcessHitResult(HitResult.Sunken);
            Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
        }

    }
}