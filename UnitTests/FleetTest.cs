using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class FleetTest
    {
        [TestMethod]
        public void ConstructionOfTheFleetConstructsFleetWithZeroShips()
        {
            Fleet fleet = new Fleet();

            Assert.AreEqual(0, fleet.Ships.Count());
        }

        [TestMethod]
        public void CallToCreateShipAddsNewShipToFleet()
        {
            Fleet fleet = new Fleet();
            List<Square> ship = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(ship);

            Assert.AreEqual(1, fleet.Ships.Count());

            ship = new List<Square> { new Square(5, 7), new Square(6, 7), new Square(7, 7) };
            fleet.CreateShip(ship);

            Assert.AreEqual(2, fleet.Ships.Count());
        }

        [TestMethod]
        public void HitForFleetReturnsMissedIfSquareDoesntBelongToAnyShip()
        {
            Fleet fleet = new Fleet();
            List<Square> ship = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };

            fleet.CreateShip(ship);

            Assert.AreEqual(HitResult.Missed, fleet.Hit(new Square(2, 2)));
            Assert.AreEqual(HitResult.Missed, fleet.Hit(new Square(1, 1)));
        }

        [TestMethod]
        public void HitForFleetReturnsHitIfSquareDoesntBelongesToShip()
        {
            Fleet fleet = new Fleet();
            List<Square> ship = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };

            fleet.CreateShip(ship);

            Assert.AreEqual(HitResult.Hit, fleet.Hit(new Square(1, 2)));
            Assert.AreEqual(HitResult.Hit, fleet.Hit(new Square(1, 3)));
        }

        [TestMethod]
        public void HitForFleetReturnsSunkenIfAllSquaresBelongingToShipAreSunken()
        {
            Fleet fleet = new Fleet();
            List<Square> ship = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };

            fleet.CreateShip(ship);

            fleet.Hit(new Square(1, 2));
            fleet.Hit(new Square(1, 3));

            Assert.AreEqual(HitResult.Sunken, fleet.Hit(new Square(1, 4)));
        }

        [TestMethod]
        public void HitForFleetReturnsHitIfSomeOfTheSquaresAreHit()
        {
            Fleet fleet = new Fleet();
            List<Square> ship = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            List<Square> shipTwo = new List<Square> { new Square(5, 7), new Square(6, 7), new Square(7, 7) };
            List<Square> shipThree = new List<Square> { new Square(8, 1), new Square(9,1) };

            fleet.CreateShip(ship);
            fleet.CreateShip(shipTwo);
            fleet.CreateShip(shipThree);

            fleet.Hit(new Square(5, 7));
            fleet.Hit(new Square(6, 7));
            fleet.Hit(new Square(7, 7));

            fleet.Hit(new Square(8, 1));
            Assert.AreEqual(HitResult.Sunken, fleet.Hit(new Square(9, 1)));
        }
    }
}
