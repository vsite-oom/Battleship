using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

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
            
            Square square1 = new Square(2, 5);
            Square square2 = new Square(2, 6);
            Square square3 = new Square(2, 7);

            Ship ship = new Ship(new List<Square> { square1, square2, square3 });

            var result = ship.Hit(square1);
            result = ship.Hit(square2);
            result = ship.Hit(square3);

            Assert.AreEqual(SquareState.Sunken, square1.SquareState);
            Assert.AreEqual(SquareState.Sunken, square2.SquareState);
            Assert.AreEqual(SquareState.Sunken, square3.SquareState);



        }

    }
}
