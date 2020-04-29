using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestShipwright
    {
        [TestMethod]
        public void CreateFleetMethodCreatesShip()
        {
            shipwright sw = new shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
        [TestMethod]
        public void CreateFleetMethodCreatesShipsForAGivenTerminator()
        {
            var terminator = SquareTerminatorFactory.Create(ShipAdJoining.None,10,10);
            shipwright sw = new shipwright(10, 10,terminator);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
    }
}
