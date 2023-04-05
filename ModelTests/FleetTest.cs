using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class FleetTest
    {
        [TestMethod]
        public void CreateShipAddsNewShipToFleet()
        {
            Fleet fleet = new();
            Square[] squares = { new Square(1, 2), new Square(1, 2), new Square(1, 4) };
            fleet.createShip(squares);

            Assert.AreEqual(1, fleet.Ships.Count());
            var ship = fleet.Ships.ElementAt(0);
            Assert.IsTrue(ship.Squares.Contains(squares[0]));
            Assert.IsTrue(ship.Squares.Contains(squares[1]));
            Assert.IsTrue(ship.Squares.Contains(squares[2]));

        }
    }
}