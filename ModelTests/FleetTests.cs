using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void CreateShipAddsNewShipToFleet()
        {
            var fleet = new Fleet();
            Square[] sqaures = { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            
            fleet.CreateShip(sqaures);

            Assert.AreEqual(1, fleet.Ships.Count());

            var ship = fleet.Ships.ElementAt(0);
            Assert.IsTrue(ship.Squares.Contains(sqaures[0]));
            Assert.IsTrue(ship.Squares.Contains(sqaures[1]));
            Assert.IsTrue(ship.Squares.Contains(sqaures[2]));
        }
    }
}