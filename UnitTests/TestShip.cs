using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShip
    {
        [TestMethod]
        public void ConstructorCreatesShipFromSquaresProvided()
        {
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            var ship = new Ship(squares);
            Assert.IsTrue(ship.Squares.Contains(squares[0]));
            Assert.IsTrue(ship.Squares.Contains(squares[1]));
            Assert.IsTrue(ship.Squares.Contains(squares[2]));
        }

        [TestMethod]
        public void SquaresContainsOnlyGivenElementsInConstructor()
        {
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            var ship = new Ship(squares);
            Assert.IsFalse(ship.Squares.Contains(new Square(5, 4)));
            Assert.IsFalse(ship.Squares.Contains(new Square(1, 1)));
            Assert.IsFalse(ship.Squares.Contains(new Square(1, 5)));
        }

        [TestMethod]
        public void HitReturnesHitIfSquareProvidedBelongToShip()
        {
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(1, 2)));
        }

        [TestMethod]
        public void HitReturnesSunkenIfSquareProvidedIsLastRemainingSquareBelongingToShip()
        {
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(1, 2)));
            Assert.AreEqual(HitResult.Hit, ship.Hit(new Square(1, 4)));
            Assert.AreEqual(HitResult.Sunken, ship.Hit(new Square(1, 3)));
        }

        [TestMethod]
        public void HitReturnesMissedtIfSquareProvidedDoesNotBelongToShip()
        {
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            var ship = new Ship(squares);

            Assert.AreEqual(HitResult.Missed, ship.Hit(new Square(5, 4)));
        }
    }
}