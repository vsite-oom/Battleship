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
            Ship ship = new Ship(new List<Square> { new Square(3, 4), new Square(3, 5), new Square(3, 6) });
            Assert.AreEqual(ship.Hit(new Square(3, 4)), HitResult.Hit);
            Assert.AreEqual(ship.Hit(new Square(3, 6)), HitResult.Hit);
            Assert.AreEqual(ship.Hit(new Square(3, 5)), HitResult.Sunken);

            foreach (var s in ship.Squares)
            {
                Assert.AreEqual(SquareState.Sunken, s.SquareState);
            }
        }
    }
}
