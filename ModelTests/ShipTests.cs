using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vste.oom.battleship.model;

namespace vsite.oom.battleship.model.tests
{
	[TestClass]
	public class ShipTests
	{
		[TestMethod]
		public void ContructorCreatesShipWithSquaresProvided()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.IsTrue(ship.Contains(1, 4));

		}

		[TestMethod]
		public void HitMethodReturnsMissedIfSquareIsNotPartOfShip()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.AreEqual(hitResult.Missed, ship.Hit(2, 4));

		}
		[TestMethod]
		public void HitMethodReturnsHitIfSquareIsPartOfShip()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 5));


		}
		[TestMethod]
		public void HitMethodReturnsSunkenAfterLastSquareIsHit()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 5));

			Assert.AreEqual(hitResult.Sunken, ship.Hit(1, 4));


		}
		[TestMethod]
		public void HitMethodReturnsHitAfterSquareIsHitAgain()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 5));

			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 3));


		}
		[TestMethod]
		public void HitMethodReturnsSunkenAfterShipIsSunkenButSquareIsHitAgain()
		{
			var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
			var ship = new Ship(squares);

			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 3));
			Assert.AreEqual(hitResult.Hit, ship.Hit(1, 5));

			Assert.AreEqual(hitResult.Sunken, ship.Hit(1, 4));
			Assert.AreEqual(hitResult.Sunken, ship.Hit(1, 5));

		}

	}
}