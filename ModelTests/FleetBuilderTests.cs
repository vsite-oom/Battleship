namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class FleetBuilderTests
{
    [TestMethod]
    public void CreateFleetBuildsFleetWithNumberOfShipsProvided()
    {
        var rows = 10;
        var cols = 10;
        int[] shipLengths = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

        var builder = new FleetBuilder(rows, cols, shipLengths);
        var fleet = builder.CreateFleet();

        Assert.AreEqual(10, fleet.Ships.Count());
    }

    [TestMethod]
    public void CreateFleetBuildsFleetWithShipsOfLengthProvided()
    {
        var rows = 10;
        var cols = 10;
        int[] shipLengths = { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

        var builder = new FleetBuilder(rows, cols, shipLengths);
        var fleet = builder.CreateFleet();

        Assert.AreEqual(4, fleet.Ships.Count(s => s.Squares.Count() == 2));
    }
}