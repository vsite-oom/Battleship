namespace Vsite.Oom.Battleship.Model.Tests;

[TestClass]
public class SquareTests
{
    [TestMethod]
    public void ConstructorCreatesSquareWithRowAndColumnProvided()
    {
        var row = 4;
        var col = 8;
        var square = new Square(row, col);

        Assert.AreEqual(row, square.Row);
        Assert.AreEqual(col, square.Column);
    }
}