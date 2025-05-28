using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Model;

namespace ModelTests;



[TestClass]
public class FleetGridTests
{
    [TestMethod]
    public void GetAvailablePlacementsForGrid1x6Returns3PlacementsForShipWith2SquaresAfterSquareInColumn3IsEliminated()
    {
        int rows = 1;
        int cols = 6;
        int shipLength = 2;
        var grid = new FleetGrid(rows, cols);
        grid.EliminateSquare(0, 3);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }
    [TestMethod]
    public void GetAvailablePlacementsForGrid8x1Returns3PlacementsForShipWith2SquaresAfterSquaresInRows3And5AreEliminated()
    {
        int rows = 8;
        int cols = 1;
        int shipLength = 2;
        var grid = new FleetGrid(rows, cols);
        
        grid.EliminateSquare(3, 0);
        grid.EliminateSquare(5, 0);

        Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
    }

}
