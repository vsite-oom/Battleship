using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWithGivenNumerOfSquares()
        {
            int rows = 10; int columns = 10;
            Grid grid = new Grid(rows,columns);
            Assert.AreEqual(columns * rows, grid.AvailableSquares().Count());
        }
    }
}