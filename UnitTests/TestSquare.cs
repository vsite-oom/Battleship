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
        public void WhenShhipIsSunkenAllSquaresAreMarkedSunken()
        {
            Ship ship = new Ship(new List<Square> { new Square(9, 9), new Square(8, 9), new Square(7, 9) });
            ship.Hit(new Square(9, 9));
            ship.Hit(new Square(8, 9));
            ship.Hit(new Square(7, 9));

            foreach (var s in ship.Squares)
                Assert.AreEqual(SquareState.Sunken, s.SquareState);
        }
    }
}
