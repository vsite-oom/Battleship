using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Vsite.Oom.Battleship.Model.Ship;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void ConstructorCreatesShipWithSquaresProvided()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);

            Assert.IsTrue(ship.Contains(1, 4));
        }
        [TestMethod]
        public void HitMethodReturnsMissedIfSquareIsNotPartOfShip()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Missed, ship.Hit(2, 4));
        }

        [TestMethod]
        public void HitMethodReturnsHitIfSquareIsPartOfShip()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));
        }

        [TestMethod]
        public void HitMethodReturnsSunkenAfterLastSquareIsHit()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);


            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));

            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 4));
        }

        [TestMethod]
        public void HitMethodReturnsHitAfterSquareIsHitAgain()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);


            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));

            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
        }

        [TestMethod]
        public void HitMethodReturnsSunkenAfterShipIsSunkenButSquareIsHitAgain()
        {
            var squares = new List<Square>
            {
                new Square(1, 3),
                new Square(1, 4),
                new Square(1, 5)
            };

            var ship = new Ship(squares);


            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 5));

            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 4));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 5));
        }
    }
}