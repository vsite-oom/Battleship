using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void ConstructorCreatesShipWithProvided()
        {
            var squares = new List <Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            var ship = new Ship(squares);

            Assert.IsTrue(ship.Contains(1, 4));
        }

        [TestMethod]
        public void HitMethodReturnsMissedIfSquaereIsNotPartOfShip()
        {
            var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Missed, ship.Hit(2, 4));
        }

        [TestMethod]
        public void HitMethodReturnsMissedIfSquaereIsPartOfShipHit()
        {
            var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));

            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 4));
        }

        [TestMethod]
        public void HitMethodReturnsSunkenIfSquaereIsPartSquareIsHitAgain()
        {
            var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));

            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 4));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 5));
        }
    }
}