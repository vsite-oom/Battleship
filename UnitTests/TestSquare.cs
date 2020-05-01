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
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {

            
            Ship ship = new Ship(new List<Square> { new Square(5, 1), new Square(5, 2), new Square(5, 3) });

            ship.Hit(new Square(5, 1));
            ship.Hit(new Square(5, 2));
            ship.Hit(new Square(5, 3));

            foreach (var square in ship.squares)
            {
                Assert.AreEqual(SquareState.Sunken, square.SquareState);
            }
            
        }

    }
}
