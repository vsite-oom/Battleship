using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void CreateShipAddsNewShipToFleet()
        {
            var fleet = new Fleet();
            Square[] squares = { new Square(1, 2), new Square(1, 3), new Square(1, 4) };
            fleet.CreateShip(squares);

            Assert.AreEqual(1, fleet.Ships.Count());

            var ship = fleet.Ships.ElementAt(0);
            Assert.IsTrue(ship.Squares.Contains(squares[0]));
            Assert.IsTrue(ship.Squares.Contains(squares[1]));
            Assert.IsTrue(ship.Squares.Contains(squares[2]));

        }
    }
}