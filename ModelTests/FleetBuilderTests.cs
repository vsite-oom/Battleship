using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class FleetBuilderTests
    {
        [TestMethod]
        public void CreateFleetBuildsFleetWithNumberOfShipsProvided()
        {
            int rows = 10;int cols = 10;
            int[] shipLen = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

            var builder=new FleetBuilder(rows, cols, shipLen);
            var fleet =builder.CreateFleet();

            Assert.AreEqual(10, fleet.Ships.Count());
        }
        [TestMethod]
        public void CreateFleetBuildsFleetWithShipsOfLengthProvided()
        {
            int rows = 10; int cols = 10;
            int[] shipLen = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

            var builder = new FleetBuilder(rows, cols, shipLen);
            var fleet = builder.CreateFleet();

            Assert.AreEqual(4, fleet.Ships.Count(s => s.Squares.Count() == 2));
        }
    }
}