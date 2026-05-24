using Battleship.Model;

namespace Battleship.Tests;

[TestClass]
public sealed class ShotsGridTests
{
    [TestMethod]
    public void GetSquaresInDirectionReturns3SquaresAboveSquare3x3()
    {
        var grid = new ShotsGrid(10, 10);
        int row = 3;
        int column = 3;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
        Assert.HasCount(3, squares);
    }
    [TestMethod]
    public void GetSquaresInDirectionReturns4SquaresRightFromSquare3x5()
    {
        var grid = new ShotsGrid(10, 10);
        int row = 3;
        int column = 5;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
        Assert.HasCount(4, squares);
    }
    [TestMethod]
    public void GetSquaresInDirectionReturns2SquaresBelowSquare7x5()
    {
        var grid = new ShotsGrid(10, 10);
        int row = 7;
        int column = 5;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
        Assert.HasCount(2, squares);
    }
    [TestMethod]
    public void GetSquaresInDirectionReturns1SquareLeftFromSquare7x1()
    {
        var grid = new ShotsGrid(10, 10);
        int row = 7;
        int column = 1;
        var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
        Assert.HasCount(1, squares);
    }
}