using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestFleet
    {
        [TestMethod]
        public void ConstructorCreatesEmptyFleet()
        {
            Fleet fleet = new Fleet();
            Assert.AreEqual(0, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateShipAddsNewShipToFleet()
        {
            Fleet fleet = new Fleet();
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);
            Assert.AreEqual(1, fleet.Ships.Count());

            squares = new List<Square> { new Square(5, 7), new Square(6, 7), new Square(7, 7) };
            fleet.CreateShip(squares);
            Assert.AreEqual(2, fleet.Ships.Count());
        }

        [TestMethod]
        public void HitForFleetReturnsMissedIfSquareDoesntBelongToAny()
        {
            Fleet fleet = new Fleet();
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);

            HitResult result = fleet.Hit(new Square(2, 2));
            Assert.AreEqual(HitResult.Missed, result);

            result = fleet.Hit(new Square(5, 5));
            Assert.AreEqual(HitResult.Missed, result);
        }

        [TestMethod]
        public void HitForFleetReturnsHitIfSquareBelongsToAnyShip()
        {
            Fleet fleet = new Fleet();
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);

            HitResult result = fleet.Hit(new Square(1, 2));
            Assert.AreEqual(HitResult.Hit, result);

            result = fleet.Hit(new Square(1, 4));
            Assert.AreEqual(HitResult.Hit, result);
        }

        [TestMethod]
        public void HitForFleetReturnsSunkenIfAllSquaresBelongingToAShipAreHit()
        {
            Fleet fleet = new Fleet();
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);

            fleet.Hit(new Square(1, 2));
            fleet.Hit(new Square(1, 4));
            HitResult result = fleet.Hit(new Square(1, 3));
            Assert.AreEqual(HitResult.Sunken, result);
        }

        [TestMethod]
        public void HitForFleetReturnsSunkenIfAllSquaresBelongingToOneOfShipsAreHit()
        {
            Fleet fleet = new Fleet();
            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);
            squares = new List<Square> { new Square(5, 7), new Square(6, 7), new Square(7, 7) };
            fleet.CreateShip(squares);
            squares = new List<Square> { new Square(8, 1), new Square(9, 1) };
            fleet.CreateShip(squares);

            fleet.Hit(new Square(5, 7));
            fleet.Hit(new Square(6, 7));
            HitResult result = fleet.Hit(new Square(7, 7));
            Assert.AreEqual(HitResult.Sunken, result);

            fleet.Hit(new Square(8, 1));
            result = fleet.Hit(new Square(9, 1));
            Assert.AreEqual(HitResult.Sunken, result);
        }
    } 
}
