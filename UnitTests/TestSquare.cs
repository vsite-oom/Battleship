using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.BattleShip.Model.UnitTests
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void SquareConstructorCreatesSquareWithGivenPosition()
        {
            Square s = new Square(1, 8);
            Assert.AreEqual(1, s.row);
            Assert.AreEqual(8, s.column);
        }

        [TestMethod]
        public void WhenShipIsSunkAllSquaresAreMarkedSunk()
        {
            Ship ship = new Ship(new List < Square >{ new Square(1,1), new Square(1,2), new Square(1,3)});
            foreach (Square s in ship.Squares)
                ship.IsHit(s);
            Assert.AreEqual(HitResult.Sunk, ship.IsHit(new Square(1, 1)));
            Assert.AreEqual(HitResult.Sunk, ship.IsHit(new Square(1, 2)));
            Assert.AreEqual(HitResult.Sunk, ship.IsHit(new Square(1, 3)));
        }
    }
}
