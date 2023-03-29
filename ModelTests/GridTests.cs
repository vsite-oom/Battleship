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
        [TestMethod]
        public void GetAvaliableSequancesReturnsTwoSequencesOfLength3ForGrid1Row4Columns()
        {
            int rows = 1; int columns = 4;
            Grid grid = new Grid(rows, columns);
            var result = grid.GetAvaliableSequences(3);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void GetAvaliableSequancesReturnsThreeSequencesOfLength3ForGrid5Rows1Columns()
        {
            int rows = 1; int columns = 4;
            Grid grid = new Grid(rows, columns);
            var result = grid.GetAvaliableSequences(3);
            Assert.AreEqual(3, result.Count());
        }



    }
}