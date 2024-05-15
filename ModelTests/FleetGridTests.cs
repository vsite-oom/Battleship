namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class FleetGridTests
{
    [TestMethod]
    public void ConstructorCreatesGridWith50SquaresFor5RowsAnd10Columns()
    {
        var rows = 5;
        var cols = 10;

        var grid = new FleetGrid(rows, cols);

        Assert.AreEqual(50, grid.Squares.Count());
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
    {
        var shipLength = 3;
        var grid = new FleetGrid(1, 5);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid7x1Returns6PlacementsForShipWith2Squares()
    {
        var rows = 7;
        var cols = 1;
        var shipLength = 2;
        var grid = new FleetGrid(rows, cols);
        Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
    }

    [TestMethod]
    public void GetAvailablePlacementsForGrid5x5Returns20PlacementsForShipWith4Squares()
    {
        var rows = 5;
        var cols = 5;
        var shipLength = 4;
        var grid = new FleetGrid(rows, cols);
        Assert.AreEqual(20, grid.GetAvailablePlacements(shipLength).Count());
    }


    [TestMethod]
    public void GetAvailablePlacementsForGrid1x6Returns3PlacementsForShipWith2SquaresAfterSquareInColumn3IsEliminated()
    {
        var rows = 1;
        var cols = 6;
        var shipLength = 2;
        var grid = new FleetGrid(rows, cols);
        grid.EliminateSquare(0, 3);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }


    [TestMethod]
    public void
        GetAvailablePlacementsForGrid8x1Returns3PlacementsForShipWith2SquaresAfterSquareInColumn3And5AreEliminated()
    {
        var rows = 8;
        var cols = 1;
        var shipLength = 2;
        var grid = new FleetGrid(rows, cols);
        grid.EliminateSquare(3, 0);
        grid.EliminateSquare(5, 0);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }
}