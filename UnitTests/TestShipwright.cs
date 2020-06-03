using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Vsite.Oom.Battleship.Model.SquareTerminatorFactory;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestShipwright
    {
        public object ShipAdJoininig { get; private set; }

        [TestMethod]
        public void CreateFleetMethodCreatesShips()
        {
            Shipwright sw = new Shipwright(10, 10);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateFleetMethodCreatsShipsForAGivenTerminator()
        {
            var terminator = SquareTerminatorFactory.Create(ShipAdjoining.None, 10, 10);
            Shipwright sw = new Shipwright(10, 10, terminator);
            var fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            Assert.AreEqual(10, fleet.Ships.Count());
        }
    }
}