using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestFleet
    {
        [TestMethod]
        public void CreateShipAddsNewShipFromSquaresProvidedToTheFleet()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Fleet fleet = new Fleet();
            Assert.AreEqual(0, fleet.Ships.Count());
            fleet.CreateShip(squares);
            Assert.AreEqual(1, fleet.Ships.Count());

            squares = new List<Square> { new Square(3, 8), new Square(4, 8) };
            fleet.CreateShip(squares);
            Assert.AreEqual(2, fleet.Ships.Count());
        }

        [TestMethod]
        public void HitMethodReturnsMissedForASquareNotBelongingToAnyShip()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Fleet fleet = new Fleet();
            fleet.CreateShip(squares);

            squares = new List<Square> { new Square(3, 8), new Square(4, 8) };
            fleet.CreateShip(squares);

            Assert.AreEqual(HitResult.Missed, fleet.Hit(new Square(3, 9)));
            Assert.AreEqual(HitResult.Missed, fleet.Hit(new Square(0, 0)));
        }

        [TestMethod]
        public void HitMethodReturnsHitForASquareThatBelongsToOneOfShips()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            Fleet fleet = new Fleet();
            fleet.CreateShip(squares);

            squares = new List<Square> { new Square(3, 8), new Square(4, 8) };
            fleet.CreateShip(squares);

            Assert.AreEqual(HitResult.Hit, fleet.Hit(new Square(4, 8)));
            Assert.AreEqual(HitResult.Hit, fleet.Hit(new Square(4, 4)));
            Assert.AreEqual(HitResult.Hit, fleet.Hit(new Square(4, 5)));
        }
    }
}
