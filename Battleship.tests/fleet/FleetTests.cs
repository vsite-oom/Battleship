using Battleship.Model;

namespace Battleship.Tests;

[TestClass]
public sealed class FleetTests
{
    [TestMethod]
    public void ConstructorCreatesEmptyFleet()
    {
        var fleet = new Fleet();

        Assert.IsEmpty(fleet.Ships);
    }

    [TestMethod]
    public void CreateShipAddsNewShipToFleet()
    {
        var fleet = new Fleet();

        var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };

        fleet.CreateShip(squares);

        Assert.HasCount(1, fleet.Ships);
    }

    [TestMethod]
    public void HitMethodReturnsMissedForSquareNotInAnyShip()
    {
        Fleet fleet = CreateFleet();

        Assert.AreEqual(HitResult.Missed, fleet.Hit(0, 0));
    }

    [TestMethod]
    [DataRow(1, 3)]
    [DataRow(8, 4)]
    [DataRow(1, 4)]
    public void HitMethodReturnsHitForSquareBelongingToAnyShip(int row, int column)
    {
        Fleet fleet = CreateFleet();

        Assert.AreEqual(HitResult.Hit, fleet.Hit(row, column));
    }

    [TestMethod]
    public void HitMethodReturnsSunkenAfterLastSquareInFirstShipIsHit()
    {
        Fleet fleet = CreateFleet();

        fleet.Hit(1, 3);
        fleet.Hit(1, 4);
        Assert.AreEqual(HitResult.Sunken, fleet.Hit(1, 5));
    }

    [TestMethod]
    public void HitMethodReturnsSunkenAfterLastSquareInSecondShipIsHit()
    {
        Fleet fleet = CreateFleet();

        fleet.Hit(1, 3);
        fleet.Hit(1, 4);
        fleet.Hit(1, 5);

        fleet.Hit(8, 5);
        Assert.AreEqual(HitResult.Sunken, fleet.Hit(8, 4));
    }

    private Fleet CreateFleet()
    {
        var fleet = new Fleet();

        var ship1 = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };

        fleet.CreateShip(ship1);

        var ship2 = new List<Square> { new Square(8, 4), new Square(8, 5) };

        fleet.CreateShip(ship2);
        return fleet;
    }
}
