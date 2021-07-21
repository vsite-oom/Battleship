using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests {
    [TestClass]
    public class TestFleet {
        [TestMethod]
        public void ConstructorCreatesEmptyFleet() {
            Fleet fleet = new Fleet();
            Assert.AreEqual(0, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateShipAddsNewShipToFleet() {
            Fleet fleet = new Fleet();

            List<Square> squares = new List<Square> { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);
            Assert.AreEqual(1, fleet.Ships.Count());

            squares = new List<Square> { new Square(5, 7), new Square(6, 7), new Square(7, 7) };
            fleet.CreateShip(squares);
            Assert.AreEqual(2, fleet.Ships.Count());
        }
    }
}
