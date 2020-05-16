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
            Ship s = new Ship(new List<Square> { new Square(1, 1), new Square(1, 2), new Square(1, 3) });
            Assert.AreEqual(s.Hit(new Square(1, 1)), HitResult.Hit);
            Assert.AreEqual(s.Hit(new Square(1, 2)), HitResult.Hit);
            Assert.AreEqual(s.Hit(new Square(1, 3)), HitResult.Sunken);
        }
    }
}
