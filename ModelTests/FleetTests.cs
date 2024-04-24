using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void ConstructoCreatesEmptyFleet()
        {
            var fleet = new Fleet();

            Assert.AreEqual(0, fleet.Ships.Count());
        }
    }
}