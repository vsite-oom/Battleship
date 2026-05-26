using Battleship.Model;

namespace Battleship.Tests;

[TestClass]
public sealed class ShipTests
{
    private readonly Ship ship;

    public ShipTests()
    {
        ship = new Ship([new Square(1, 3), new Square(1, 4), new Square(1, 5)]);
    }

    [TestMethod]
    public void ConstructorCreatesShipWithSquaresProvided()
    {
        Assert.IsTrue(ship.Contains(1, 4));
    }

    [TestMethod]
    public void HitMethodReturnsMissedIfSquareIsNotPartOfShip()
    {
        Assert.AreEqual(HitResult.Missed, ship.Hit(2, 4));
    }

    [TestMethod]
    [DataRow(1, 3)]
    [DataRow(1, 5)]
    public void HitMethodReturnsHitIfSquareIsPartOfShip(int row, int column)
    {
        Assert.AreEqual(HitResult.Hit, ship.Hit(row, column));
    }

    [TestMethod]
    public void HitMethodReturnsSunkenAfterLastSquareIsHit()
    {
        ship.Hit(1, 3);
        ship.Hit(1, 5);
        Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 4));
    }

    [TestMethod]
    public void HitMethodReturnsHitAfterSquareIsHitAgain()
    {
        ship.Hit(1, 3);
        Assert.AreEqual(HitResult.Hit, ship.Hit(1, 3));
    }

    [TestMethod]
    public void HitMethodReturnsSunkenAfterShipIsSunkenButSquareIsHitAgain()
    {
        ship.Hit(1, 3);
        ship.Hit(1, 5);
        ship.Hit(1, 4);
        Assert.AreEqual(HitResult.Sunken, ship.Hit(1, 5));
    }
}