using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vste.oom.battleship.model;
using static vste.oom.battleship.model.ShotsGrid;

namespace vsite.oom.battleship.model.tests
{
	//[TestClass]
	//public class ShotsGridTests
	//{
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturnsFreeSquaresAboveSquare3x3()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 3;
	//		int column = 3;
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
	//		Assert.AreEqual(3, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns4SquaresRightFromSquare3x5()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 3;
	//		int column = 5;
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
	//		Assert.AreEqual(4, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns2SquaresDownBelowSquare7x5()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 7;
	//		int column = 5;
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
	//		Assert.AreEqual(2, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns1SquareLeftFromSquare7x1()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 7;
	//		int column = 1;
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
	//		Assert.AreEqual(1, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturnsFreeSquaresAboveSquare3x3IfSquare1x3IsHit()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 3;
	//		int column = 3;
	//		grid.ChangeSquareState(1, 3, SquareState.Hit);
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
	//		Assert.AreEqual(3, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns0SquaresAboveSquare3x3IfSquare2x3IsHit()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 3;
	//		int column = 3;
	//		grid.ChangeSquareState(2, 3, SquareState.Hit);
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
	//		Assert.AreEqual(0, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns4SquaresRightFromSquare3x5IfSquare3x8IsHit()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 3;
	//		int column = 5;
	//		grid.ChangeSquareState(3, 8, SquareState.Hit);
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
	//		Assert.AreEqual(2, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns1SquareDownBelowSquare7x5IfSquare9x5IsHit()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 7;
	//		int column = 5;
	//		grid.ChangeSquareState(9, 5, SquareState.Hit);
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
	//		Assert.AreEqual(1, squares.Count());
	//	}
	//	[TestMethod]
	//	public void GetSquaresInDirectionReturns0SquaresLeftFromSquare7x1IfSquare7x0IsHit()
	//	{
	//		var grid = new ShotsGrid(10, 10);
	//		int row = 7;
	//		int column = 1;
	//		grid.ChangeSquareState(7, 0, SquareState.Hit);
	//		var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
	//		Assert.AreEqual(0, squares.Count());
	//	}
	//}
}