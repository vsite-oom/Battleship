using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class FleetBuilderTests
    {
        [TestMethod]
        public void CreateFleetReturnsFleetWithRequestedShipLengths()
        {
            var gamerules= new GameRules();
            var builder =new FleetBuilder(gamerules);
            var fleet=builder.CreateFleet();
            Assert.IsNotNull(fleet);
            Assert.AreEqual(gamerules.ShipLenghts.Count(),fleet.ships.Count());
            Assert.AreEqual(gamerules.ShipLenghts.Count(n => n == 5), fleet.ships.Count(s => s.squares.Count() == 5));
            Assert.AreEqual(gamerules.ShipLenghts.Count(n => n == 4), fleet.ships.Count(s => s.squares.Count() == 4));
            Assert.AreEqual(gamerules.ShipLenghts.Count(n => n == 3), fleet.ships.Count(s => s.squares.Count() == 3));
            Assert.AreEqual(gamerules.ShipLenghts.Count(n => n == 2), fleet.ships.Count(s => s.squares.Count() == 2));
            Assert.AreEqual(gamerules.ShipLenghts.Count(n => n == 1), fleet.ships.Count(s => s.squares.Count() == 1));

        }
    }
}