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
            var gameRules = new GameRules();
            var builder = new FleetBuilder(gameRules);
            var fleet = builder.CreateFleet();

            Assert.IsNotNull(fleet);
            Assert.AreEqual(gameRules.ShipLengths.Count(), fleet.Ships.Count());
            Assert.AreEqual(gameRules.ShipLengths.Count(n => n == 5), fleet.Ships.Count(s => s.Squares.Count() == 5));
            Assert.AreEqual(gameRules.ShipLengths.Count(n => n == 4), fleet.Ships.Count(s => s.Squares.Count() == 4));
            Assert.AreEqual(gameRules.ShipLengths.Count(n => n == 3), fleet.Ships.Count(s => s.Squares.Count() == 3));
            Assert.AreEqual(gameRules.ShipLengths.Count(n => n == 2), fleet.Ships.Count(s => s.Squares.Count() == 2));
            Assert.AreEqual(gameRules.ShipLengths.Count(n => n == 1), fleet.Ships.Count(s => s.Squares.Count() == 1));
        }
    }
}