using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.battleship.Model;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GridTest
    {
        [TestMethod]
        public void ConstructorCreatesGridwith50SquaresFor5RowsAnd10Columns()
        {
            int rows = 5;
            int columns = 5;
            var grid = new Grid(rows, columns);

            Assert.AreEqual(50, grid.Squares.Count());
        }
    }
}
