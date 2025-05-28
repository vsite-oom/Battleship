using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests;

[TestClass]
public class ShipTests
{
    [TestMethod]
    public void ConstructorCreatesShipWithSquaresProvided()
    {
        var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
        var ship = new Ship(squares);

        Assert.IsTrue(ship.Contains(1, 4));
    }
}