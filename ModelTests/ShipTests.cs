namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class ShipTests
{
    [TestMethod]
    public void ConstructorCreatesShipWithSquaresProvided()
    {
        var squares = new List<Square> { new(1, 3), new(1, 4), new(1, 5) };
        var ship = new Ship(squares);

        Assert.IsTrue(ship.Contains(1, 4));
    }
}