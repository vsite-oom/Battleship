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
            Square square = new Square(1, 8);
            Assert.AreEqual(1,square.Row);
            Assert.AreEqual(8, square.Column);
        }
        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            List<Square> squares = new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) };
            Ship ship = new Ship(squares);
            foreach(var square in squares)
            {
                ship.Hit(square);
            }
            Assert.AreEqual(SquareState.Sunken, ship.Squares.First().SquareState);
        }
    }
}
