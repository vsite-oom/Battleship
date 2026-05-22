using Battleship.Model;

namespace Battleship.Tests;

[TestClass]
public sealed class SquareEliminatorTests
{
    private readonly SquareEliminator eliminator = new SquareEliminator();

    [TestMethod]
    public void ForSquares4x3To4x6Returns18SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(18, toEliminate);
        Assert.Contains(new SquareCoordinate(3, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(5, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(3, 7), toEliminate);
        Assert.Contains(new SquareCoordinate(5, 7), toEliminate);
    }

    [TestMethod]
    public void ForSquares0x3To0x4Returns8SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(0, 3), new Square(0, 4) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(8, toEliminate);
        Assert.Contains(new SquareCoordinate(0, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(1, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(0, 5), toEliminate);
        Assert.Contains(new SquareCoordinate(1, 5), toEliminate);
    }

    [TestMethod]
    public void ForSquares3x9To4x9Returns8SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(3, 9), new Square(4, 9) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(8, toEliminate);
        Assert.Contains(new SquareCoordinate(2, 8), toEliminate);
        Assert.Contains(new SquareCoordinate(2, 9), toEliminate);
        Assert.Contains(new SquareCoordinate(5, 8), toEliminate);
        Assert.Contains(new SquareCoordinate(5, 9), toEliminate);
    }

    [TestMethod]
    public void ForSquares7x5To9x5Returns12SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(7, 5), new Square(8, 5), new Square(9, 5) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(12, toEliminate);
        Assert.Contains(new SquareCoordinate(6, 4), toEliminate);
        Assert.Contains(new SquareCoordinate(6, 6), toEliminate);
        Assert.Contains(new SquareCoordinate(9, 4), toEliminate);
        Assert.Contains(new SquareCoordinate(9, 6), toEliminate);
    }

    [TestMethod]
    public void ForSquares5x0To5x1Returns9SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(5, 0), new Square(5, 1) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(9, toEliminate);
        Assert.Contains(new SquareCoordinate(4, 0), toEliminate);
        Assert.Contains(new SquareCoordinate(4, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(6, 0), toEliminate);
        Assert.Contains(new SquareCoordinate(6, 2), toEliminate);
    }

    [TestMethod]
    public void ForSquares0x0To0x1Returns6SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(0, 0), new Square(0, 1) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(6, toEliminate);
        Assert.Contains(new SquareCoordinate(0, 0), toEliminate);
        Assert.Contains(new SquareCoordinate(0, 2), toEliminate);
        Assert.Contains(new SquareCoordinate(1, 0), toEliminate);
        Assert.Contains(new SquareCoordinate(1, 2), toEliminate);
    }

    [TestMethod]
    public void ForSquares8x9To9x9Returns6SquaresIncludingSurroundingSquares()
    {
        var shipSquares = new List<Square> { new Square(8, 9), new Square(9, 9) };

        var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

        Assert.HasCount(6, toEliminate);
        Assert.Contains(new SquareCoordinate(7, 8), toEliminate);
        Assert.Contains(new SquareCoordinate(7, 9), toEliminate);
        Assert.Contains(new SquareCoordinate(9, 8), toEliminate);
        Assert.Contains(new SquareCoordinate(9, 9), toEliminate);
    }
}