using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipwright
    {
        [TestMethod]
        public void CreateFleetReturnsValidFleet()
        {
            List<int> shipLengths = new List<int> { 5, 4, 3, 2, 1 };
            var shipwright = new Shipwright(10, 10, shipLengths);
            var fleet = shipwright.CreateFleet(new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(5, fleet.Ships.Count());
        }
    }
}