using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorSquareSum()
        {
            int rows = 5;
            int cols = 10;
            var grid=new Grid(rows, cols);
            Assert.AreEqual(rows * cols, grid.squares.Count());
        }
        [TestMethod]
        public void GetAvailableHorizontalPlacmentsTest()
        {
            int rows = 1;
            int cols = 5;
            int shipLength = 3;
            var grid = new Grid(rows, cols);
            Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
        }
        public void GetAvailableVerticalPlacmentsTest()
        {
            int rows = 7;
            int cols = 1;
            int shipLength = 2;
            var grid = new Grid(rows, cols);
            Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
        }
    }
}