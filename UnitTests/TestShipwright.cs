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
        public void CreateFleetRetrurnsValidFleet()
        {
            var shipwright = new Shipwright(10, 20, new List<int> { 5, 4, 3, 2, 1 });
            var fleet = shipwright.CreateFleet();

            Assert.AreEqual(5, fleet.Ships.Count());
        }
    }
}
