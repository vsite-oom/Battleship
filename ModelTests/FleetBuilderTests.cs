using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class FleetBuilderTests
    {
        [TestMethod]
        public void CreateFleetReturnsFleetWithRequestedShiplenghts()
        {
            GameRules gameRules = new();
            FleetBuilder builder = new (gameRules);
            Fleet fleet = builder.CreateFleet();

            Assert.IsNotNull(fleet);
            Assert.AreEqual(gameRules.shipLenghts.Count(), fleet.Ships.Count());
            Assert.AreEqual(gameRules.shipLenghts.Count(n => n == 5), fleet.Ships.Count(s => s.Squares.Count() == 5));
            Assert.AreEqual(gameRules.shipLenghts.Count(n => n == 4), fleet.Ships.Count(s => s.Squares.Count() == 4));
            Assert.AreEqual(gameRules.shipLenghts.Count(n => n == 3), fleet.Ships.Count(s => s.Squares.Count() == 3));
            Assert.AreEqual(gameRules.shipLenghts.Count(n => n == 2), fleet.Ships.Count(s => s.Squares.Count() == 2));
            Assert.AreEqual(gameRules.shipLenghts.Count(n => n == 1), fleet.Ships.Count(s => s.Squares.Count() == 1));
        }
    }
}