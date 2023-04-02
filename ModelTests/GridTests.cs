using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class GridTests
    {

        [TestMethod]
        public void ConstructorCreatesGridWithGivenNumberOfSquares()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            Assert.AreEqual(rows * columns, grid.AvailableSquares().Count());
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsTwoSquencesOfLenght3ForGrid1Row4Columns()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSquences(3);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght3ForGrid5Row1Columns()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSquences(3);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght2ForGrid1Row6Columns0_2IsRemoved()
        {
            int rows = 1;
            int columns = 6;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(0, 2);
            var result = grid.GetAvailableSquences(2);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght3ForGrid5Row1Columns2()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSquences(3);
            Assert.AreEqual(3, result.Count());
        }

    }
}

