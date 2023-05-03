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

            Assert.AreEqual(1, result.Count(s => s.Contains(new Square(0,0))));
            Assert.AreEqual(2, result.Count(s => s.Contains(new Square(0,1))));
            Assert.AreEqual(2, result.Count(s => s.Contains(new Square(0,2))));
            Assert.AreEqual(1, result.Count(s => s.Contains(new Square(0,3))));
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
        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedHit()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.MarkSquare(1, 0, HitResult.Hit);
            var result = grid.GetAvailableSquences(2);
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

    }
}


