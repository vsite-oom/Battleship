using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vsite.Oom.Battleship.Model;

namespace Vsite.oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipWright
    {
        public void CreateFleetCreateShips()
        {
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateFleetMethodCreatesShipsForAGivenTerminator()
        {
            var terminator = SquareTerminatorFactory.Create(ShipAdjoining.None, 10, 10);
            Shipwright sw = new Shipwright(10, 10, terminator);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
    }
}
