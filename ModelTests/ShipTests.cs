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
	}
}