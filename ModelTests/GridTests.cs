using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests;
[TestClass]
public class GridTests
{
    [TestMethod]
    public void ConstructorCreatesGridWith50SquaresFor5RowsAnd10Columns(Assert assert)
    {
        int rows = 5;
        int columns = 10;

        var grid = new Grid(rows, columns);
        assert.AreEqual(50, grid.Squares.Count());
    }


}
