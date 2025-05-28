using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests;

[TestClass]
public class FleetTests
{
    [TestMethod]
    public void ConstructorCreatesEmptyFleet()
    {
        var fleet = new Fleet();

        Assert.AreEqual(0, fleet.Ships.Count());
    }

    [TestMethod]
    public void CreateShipAddsNewShipToFleet()
    {
        var fleet = new Fleet();

        var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };

        fleet.CreateShip(squares);

        Assert.AreEqual(1, fleet.Ships.Count());
    }
}
