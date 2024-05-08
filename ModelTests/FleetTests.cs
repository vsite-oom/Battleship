using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vste.oom.battleship.model.tests
{
	[TestClass]
	public class FleetTests
	{
		[TestMethod]
		public void ConstructorCreatesEmptyFleet()
		{
			var fleet = new Fleet();

			Assert.AreEqual(0, fleet.Ships.Count());
		}
		
		[TestMethod]
		public void CreateShipAddsNewShipToFleet()
		{
			var fleet = new Fleet();
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };

			fleet.CreateShip(squares);

			Assert.AreEqual(1, fleet.Ships.Count());
		}
		[TestMethod]
		public void HitMethodReturnsMissedForSquareNotInAnyShip()
		{
			Fleet fleet = CreateFleet();

			Assert.AreEqual(hitResult.Missed, fleet.Hit(0,0));
		}
		[TestMethod]
		public void HitMethodReturnsHitForSquareBelongingToAnyShip()
		{
			Fleet fleet = CreateFleet();

			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, fleet.Hit(8, 4));
			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 4));
		}
		[TestMethod]
		public void HitMethodReturnsSunkenAfterLastSquareInFirstShipIsHit()
		{
			Fleet fleet = CreateFleet();

			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 4));
			Assert.AreEqual(hitResult.Sunken, fleet.Hit(1, 5));
		}
		[TestMethod]
		public void HitMethodReturnsSunkenAfterLastSquareInecondShipIsHit()
		{
			Fleet fleet = CreateFleet();

			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, fleet.Hit(1, 4));
			Assert.AreEqual(hitResult.Sunken, fleet.Hit(1, 5));

			Assert.AreEqual(hitResult.Hit, fleet.Hit(8, 4));
			Assert.AreEqual(hitResult.Sunken, fleet.Hit(8, 5));
		}





		private Fleet CreateFleet()
		{
			var fleet = new Fleet();
			var ship1 = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };

			fleet.CreateShip(ship1);

			var ship2 = new List<Square> { new Square(8, 4), new Square(8, 5) };

			fleet.CreateShip(ship2);
			return fleet;
		}
	}
}