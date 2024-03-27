using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vste.oom.battleship.model;

namespace vsite.oom.battleship.model.tests
{
	[TestClass]
	public class SquareTests
	{
		[TestMethod]
		public void ConstructorCreatesSquareWithRowAndColumnProvided()
		{
			int row = 4;
			int col = 8;
			var square = new Square(row, col);

			Assert.AreEqual(row, square.Row);
			Assert.AreEqual(col, square.Column);
		}
	}
}