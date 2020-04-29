using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
	[TestClass]
	public class TestSquare
	{
		[TestMethod]
		public void SquareConstructorCreatesSquareWithGivenPosition()
		{
			Square s = new Square(1, 8);
			Assert.AreEqual(1, s.Row);
			Assert.AreEqual(8, s.Column);
		}

		[TestMethod]
		public void WhenShipIsSunkAllSquaresAreMarkedSunk()
		{
			Ship ship = new Ship(new List<Square>{new Square(1,1), new Square(1,2)
				,new Square(1,3) });

			foreach (Square square in ship.Squares)
			{
				ship.Hit(square);
			}

			Assert.AreEqual(HitResult.Sunk, ship.Hit(new Square(1, 1)));
			Assert.AreEqual(HitResult.Sunk, ship.Hit(new Square(1, 2)));
			Assert.AreEqual(HitResult.Sunk, ship.Hit(new Square(1, 3)));

		}
	}
}
