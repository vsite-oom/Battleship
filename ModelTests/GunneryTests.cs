using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vste.oom.battleship.model;

namespace vsite.oom.battleship.model.tests
{
	[TestClass]
	public class GunneryTests
	{
		[TestMethod]
		public void InitialShootingTacticsIsRandom()
		{
			var gunnery = new Gunnery(10, 10, new List<int>{1, 2, 3});
			Assert.AreEqual(shootingTacticts.Random, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsRemainsRandomIfHitIsMissed()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Missed);
			Assert.AreEqual(shootingTacticts.Random, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsChangesToSurroundingAfterFirstSquareIsHit()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsRemainsSurroundingIfNextSquareIsMissed()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
			//gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Missed);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsChangesFromSurroundingToInlineAfterSecondSquareIsHit()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Inline, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsRemainsIlineAfterThirdSquareIsHit()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Inline, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Inline, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsRemainsIlineAfterThirdSquareIsMissed()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Inline, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Missed);
			Assert.AreEqual(shootingTacticts.Inline, gunnery.shootingTacticts);
		}
		[TestMethod]
		public void ShootingTacticsChangesToRandomAfterShipIsSunken()
		{
			var gunnery = new Gunnery(10, 10, new List<int> { 1, 2, 3 });
			gunnery.Next();
			gunnery.ProcessHitResult(hitResult.Hit);
			Assert.AreEqual(shootingTacticts.Surrounding, gunnery.shootingTacticts);
			gunnery.ProcessHitResult(hitResult.Sunken);
			Assert.AreEqual(shootingTacticts.Random, gunnery.shootingTacticts);
		}
	}
}