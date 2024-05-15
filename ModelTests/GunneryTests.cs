﻿namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class GunneryTests
{
    [TestMethod]
    public void InitialShootingTacticsIsRandom()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsRemainsRandomIfHitResultIsMissed()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Missed);
        Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsChangesToSurroundingAfterFirstSquareIsHit()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsChangesToSurroundingIfNextSquareIsMissed()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        gunnery.ProcessHitResult(HitResult.Missed);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsChangesFromSurroundingToInlineAfterSecondSquareIsHit()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsChangesRemainsInlineAfterThirdSquareIsHit()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Inline, gunnery.ShootingTactics);
    }

    [TestMethod]
    public void ShootingTacticsChangesRemainsInlineAfterThirdSquareIsMissed()
    {
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
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
        var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
        gunnery.ProcessHitResult(HitResult.Hit);
        Assert.AreEqual(ShootingTactics.Surrounding, gunnery.ShootingTactics);
        gunnery.ProcessHitResult(HitResult.Sunken);
        Assert.AreEqual(ShootingTactics.Random, gunnery.ShootingTactics);
    }
}