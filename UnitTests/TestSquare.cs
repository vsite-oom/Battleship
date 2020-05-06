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
            Assert.AreEqual(8, s.Column);
        }

        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            Ship ship = new Ship(new List<Square> { new Square(2, 4), new Square(3, 4), new Square(4, 4) });
            ship.Hit(new Square(2, 4));
            ship.Hit(new Square(3, 4));
            ship.Hit(new Square(4, 4));

            foreach (var s in ship.Squares)
                Assert.AreEqual(SquareState.Sunken, s.SquareState);
        }
    }
}