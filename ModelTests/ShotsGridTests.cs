using System.Data.Common;

namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class ShotsGridTests
{
    [TestMethod]
    public void GetSquaresInDirectionResturns3SquaresAboveSquare3x3()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 3;
        var column = 3;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
        Assert.AreEqual(3, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturns4SquaresRightSquare3x5()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 3;
        var column = 5;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Right);
        Assert.AreEqual(4, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturns2SquaresDownSquare7x5()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 7;
        var column = 5;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
        Assert.AreEqual(2, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturns1SquaresLeftSquare7x1()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 7;
        var column = 1;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Left);
        Assert.AreEqual(1, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionResturns1SquaresAboveSquare3x3IfSquare1x3IsHit()
    {
        var grid = new ShotsGrid(10, 10);
        grid.ChangeSquareState(1, 3, SquareState.Hit);
        var row = 3;
        var column = 3;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
        Assert.AreEqual(1, squares.Count());
    }
    [TestMethod]
    public void GetSquaresInDirectionResturnsZeroSquaresAboveSquare3x3IfSquare2x3IsHit()
    {
        var grid = new ShotsGrid(10, 10);
        grid.ChangeSquareState(2, 3, SquareState.Hit);
        var row = 3;
        var column = 3;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
        Assert.AreEqual(0, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturns2SquaresRightSquare3x5IfSquare3x8()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 3;
        var column = 5;
        grid.ChangeSquareState(3, 8, SquareState.Hit);
        var squares = grid.GetSquaresInDirection(row, column, Direction.Right);
        Assert.AreEqual(2, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturns1SquareDownSquare7x5IfSquare9x5IsHit()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 7;
        var column = 5;
        grid.ChangeSquareState(9, 5, SquareState.Hit);
        var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
        Assert.AreEqual(1, squares.Count());
    }

    [TestMethod]
    public void GetSquaresInDirectionReturnsZeroSquaresLeftSquare7x1IfSquare7x0IsHit()
    {
        var grid = new ShotsGrid(10, 10);
        var row = 7;
        var column = 1;
        grid.ChangeSquareState(7, 0, SquareState.Hit);
        var squares = grid.GetSquaresInDirection(row, column, Direction.Left);
        Assert.AreEqual(0, squares.Count());
    }
}