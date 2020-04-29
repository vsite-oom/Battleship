using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Linq;
namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipwright
    {
        [TestMethod]
        public void CreateFleetCreatesShips()
        {
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateFleetCreatesShipsForAGivenTerminator()
        {
            var terminator = SquareTerminatorFactory.Create(ShipADjoining.None, 10, 10);
            Shipwright sw = new Shipwright(10, 10,terminator);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
    }
}
