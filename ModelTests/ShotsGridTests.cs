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
}