using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
	[TestClass]
	public class TestSquare
	{
		[TestMethod]
		public void SquareConstructorCreateSquareWithGivenPosition()
		{
			Square s = new Square(1, 8);
			Assert.AreEqual(1, s.Row);
			Assert.AreEqual(8, s.Col);
		}

		[TestMethod]
		public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
		{
			Ship ship = new Ship(new List<Square> { new Square(1, 4)});
			var result = ship.Hit(new Square(1, 4));
			Assert.AreEqual(HitResult.Sunken, result);
		}
	}
}
