using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void CreateShipAddsNewShipToFlit()
        {
            var fleet = new Fleet();
            Square[] squares = { new Square(1, 2), new Square(1, 3), new Square(1, 4) };

            fleet.CreateShip(squares);
            Assert.AreEqual(1, fleet.ships.Count());
            var ship = fleet.ships[0];
            Assert.IsTrue(ship.squares.Contains(squares[0]));
            Assert.IsTrue(ship.squares.Contains(squares[1]));
            Assert.IsTrue(ship.squares.Contains(squares[2]));
        }
    }
}