using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWith50SquaresFor5RowsAnd10Columnes()
        {
            int rows = 5;
            int cols = 10;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(50, grid.Squares.Count());
        }

        [TestMethod]
        public void GetAvaliablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
        {
            int rows = 1;
            int cols = 5;
            int shipLength = 3;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(3, grid.GetAvaliablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvaliablePlacementsForGrid7x1Returns6PlacementsForShipWith2quares()
        {
            int rows = 7;
            int cols = 1;
            int shipLength = 2;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(6, grid.GetAvaliablePlacements(shipLength).Count());
        }
    }
}