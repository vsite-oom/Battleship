using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipwright
    {
        [TestMethod]
        public void CreateFleetMethodReturnsFleetWithShipsOfLengthsProvided()
        {
            List<int> shipLengths = new List<int> { 5, 4, 4 };
            Shipwright sw = new Shipwright(10, 10, shipLengths);
            var fleet = sw.CreateFleet();
            Assert.AreEqual(3, fleet.Ships.Count());
            Assert.AreEqual(1, fleet.Ships.Count(s => s.Squares.Count() == 5));
            Assert.AreEqual(2, fleet.Ships.Count(s => s.Squares.Count() == 4));
        }
    }
}
