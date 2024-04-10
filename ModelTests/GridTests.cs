using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWith50SquaresFor5RowsAnd10Columns()
        {
            int rows = 5;
            int columns = 10;
            Grid grid = new Grid(rows, columns);
            Assert.AreEqual(50, grid.Squares.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
        {
            int rows = 1;
            int columns = 5;
            int shipLength = 3;
            Grid grid = new(rows, columns);

            Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid7x1Returns6PlacementsForShipWith2Squares()
        {
            int rows = 7;
            int columns = 1;
            int shipLength = 2;
            Grid grid = new(rows, columns);

            Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid5x5Returns20PlacementsForShipWith4Squares()
        {
            int rows = 5;
            int columns = 5;
            int shipLength = 4;
            Grid grid = new(rows, columns);

            Assert.AreEqual(20, grid.GetAvailablePlacements(shipLength).Count());
        }
    }
}