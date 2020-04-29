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
        public void SquareConstructorCreateSquareWithGivenPosition()
        {
            Square s = new Square(1,8);
            Assert.AreEqual(1,s.Row);
            Assert.AreEqual(8,s.Column);
        }

        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            Ship ship = new Ship(new List<Square>{new Square(1,4),new Square(1,5), new Square(1,6)});
            ship.Hit(new Square(1, 5));
            ship.Hit(new Square(1, 4));
            ship.Hit(new Square(1, 6));
            Assert.IsTrue(ship.Squares.All(s=>s.SquareState == SquareState.Sunken)); 
        }
    }
}
