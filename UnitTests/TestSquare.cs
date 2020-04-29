using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Square> squares = new List<Square> { new Square(1, 1), new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            Ship ship = new Ship(squares);

            foreach (var square in squares)
            {
                ship.Hit(square);
            }

            Assert.AreEqual(SquareState.Sunken, ship.Squares.First().SquareState);
        }
    }
}
