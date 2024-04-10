using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vsite.oom.battleship.model;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWIth50SquaresFor5RowsAnd10Columns()
        {
            int rows = 5;
            int cols = 10;
            var grid = new Grid(rows, cols);
            Assert.AreEqual(50, grid.Squares.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid1x5ReturnsxPlacementsForShipWith3Squares()
        {
            int rows = 1;
            int cols = 5;
            int shipLength = 3;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(50, grid.GetAvailablePlacements(shipLength).Count());
        }
        [TestMethod]
        public void GetAvailablePlacementsForGrid7x1ReturnsxPlacementsForShipWith2Squares()
        {
            int rows = 7;
            int cols = 1;
            int shipLength = 2;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
        }  [TestMethod]
        public void GetAvailablePlacementsForGrid7x1ReturnsxPlacementsForShipWith4Squares()
        {
            int rows = 5;
            int cols = 5;
            int shipLength = 4;
            var grid = new Grid(rows, cols);

            Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
        }
    }
}

