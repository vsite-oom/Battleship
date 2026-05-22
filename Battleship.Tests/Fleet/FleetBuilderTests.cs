using Battleship.Model;

namespace Battleship.Tests;

[TestClass]
public sealed class FleetBuilderTests
{
    private readonly Fleet fleet;

    public FleetBuilderTests()
    {
        int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];
        fleet = new FleetBuilder(10, 10, shipLengths).CreateFleet();
    }

    [TestMethod]
    public void CreateFleetBuildsFleetWithNumberOfShipsProvided()
    {
        Assert.HasCount(10, fleet.Ships);
    }

    [TestMethod]
    public void CreateFleetBuildsFleetWithShipsOfLengthProvided()
    {
        Assert.AreEqual(4, fleet.Ships.Count(s => s.Squares.Count == 2));
    }
}