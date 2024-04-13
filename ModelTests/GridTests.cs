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
		[TestMethod]
		public void GetAvailablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
		{
			int rows = 1;
			int cols = 5;
			int shipLength = 3;
			var grid = new Grid(rows, cols);

			Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
		}
		//[TestMethod]
		//public void GetAvailablePlacementsForGrid7x1Returns6PlacementsForShipWith2Squares()
		//{
		//	int rows = 7;
		//	int cols = 1;
		//	int shipLength = 2;
		//	var grid = new Grid(rows, cols);

		//	Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
		//}
	}
}