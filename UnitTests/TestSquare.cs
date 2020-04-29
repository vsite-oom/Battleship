using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using static Vsite.Oom.Battleship.Model.Ship;

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
            Square s1 = new Square(1, 4);
            Square s2 = new Square(1, 5);
            Square s3 = new Square(1, 6);
            Ship ship = new Ship(new List<Square> { s1, s2, s3 });

            var result = ship.Hit(s1);
            result = ship.Hit(s2);
            result = ship.Hit(s3);

            Assert.AreEqual(SquareState.Sunken, s1.SquareState);
            Assert.AreEqual(SquareState.Sunken, s2.SquareState);
            Assert.AreEqual(SquareState.Sunken, s3.SquareState);
        }
    }
}
