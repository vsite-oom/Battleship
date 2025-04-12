using Model;

namespace ModelTests;

[TestClass]
public class GridTests
{
    [TestMethod]
    public void ConstructorCreateGridWithEnumerableSquareTest()
    {
        int row = 1;
        int column = 2;

        Grid grid = new Grid(row, column);

        Assert.AreEqual(grid.Squares.Count(), 2);
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
    {
        int rows = 1;
        int columns = 5;
        int shipLength = 3;

        var grid = new Grid(rows, columns);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid5x1Returns3PlacementsForShipWith3Squares()
    {
        int rows = 5;
        int columns = 1;
        int shipLength = 3;

        var grid = new Grid(rows, columns);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid5x5Returns20PlacementsForShipWith4Squares()
    {
        int rows = 5;
        int cols = 5;
        int shipLength = 4;
        var grid = new Grid(rows, cols);

        Assert.AreEqual(20, grid.GetAvailablePlacements(shipLength).Count());
    }
}
