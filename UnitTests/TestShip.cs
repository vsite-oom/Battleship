using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShip
    {
        [TestMethod]
        public void ConstructorCreatesAShipFromSquaresProvided()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            Assert.AreEqual(4, ship.Squares.Count());
            Assert.IsTrue(ship.Squares.Contains(squares[0]));
            Assert.IsTrue(ship.Squares.Contains(squares[1]));
            Assert.IsTrue(ship.Squares.Contains(squares[2]));
            Assert.IsTrue(ship.Squares.Contains(squares[3]));
        }

        [TestMethod]
        public void HitMethodReturnsMissedForASquareThatDoesntBelongToShip()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            Assert.AreEqual(HitResult.Missed, ship.Hit(new Square(1, 1)));
            Assert.AreEqual(HitResult.Missed, ship.Hit(new Square(4, 2)));
            Assert.AreEqual(HitResult.Missed, ship.Hit(new Square(4, 7)));
            Assert.AreEqual(HitResult.Missed, ship.Hit(new Square(7, 7)));
        }

        [TestMethod]
        public void HitMethodReturnsHitForASquareThatBelongsToShip()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 4)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 6)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 3)));
        }

        [TestMethod]
        public void HitMethodReturnsHitForASquareThatBelongsToShipAndHasAlreadyBeenHit()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 4)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 4)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 6)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 6)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 3)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(4, 3)));
        }

        [TestMethod]
        public void HitMethodReturnsSunkenForTheLastSquareHitThatBelongsToShip()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            ship.Hit(new Square(4, 4));
            ship.Hit(new Square(4, 6));
            ship.Hit(new Square(4, 3));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(4, 5)));
        }

        [TestMethod]
        public void HitMethodReturnsSunkenForTheSquareThatBelongsToSunkenShip()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Ship ship = new Ship(squares);
            ship.Hit(new Square(4, 4));
            ship.Hit(new Square(4, 6));
            ship.Hit(new Square(4, 3));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(4, 5)));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(4, 4)));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(4, 3)));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(4, 6)));
        }
    }
}
