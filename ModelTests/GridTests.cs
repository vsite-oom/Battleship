using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vste.oom.battleship.model;

namespace vsite.oom.battleship.model.tests
{
	[TestClass]
	public class GridTests
	{
		[TestMethod]
		public void ContructorCreatesGridWith50SquaresOr5RowsAnd10Columns()
		{
			int rows = 5;
			int cols = 10;
			var grid = new Grid(rows, cols);

			Assert.AreEqual(50, grid.Squares.Count());
		}
	}
}