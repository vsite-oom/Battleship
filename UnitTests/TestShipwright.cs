using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipwright
    {
        [TestMethod]
        public void CreateFleetCreateShips()
        {
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[]{ 5,4,4,3,3,3,2,2,2,2});
            Assert.AreEqual(10,fleet.Ships.Count());
        }
    
        [TestMethod]
        public void CreateFleetMethodCreateShipsForAGivenTerminator()
        {
            var terminator = SquareTerminatorFactory.Create(ShipAdjoining.None, 10, 10);
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
}
}

