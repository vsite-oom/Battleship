using System;
using System.Collections;
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
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            Ship s = new Ship(new List<Square> { new Square(1, 2), new Square(2, 2) });
            s.Hit(new Square(1, 2));
            s.Hit(new Square(2, 2));

            foreach (var square in s.Squares)
            {
                Assert.AreEqual(SquareState.Sunken, square.SquareState);

            }
        }
    }
}
