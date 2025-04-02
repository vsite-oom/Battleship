using Model;

namespace ModelTest;


[TestClass]
public class SquareTests
{
    [TestMethod]
    public void ConstructorCreateSquareWithRowAndColumnProvided()
    {
        int row = 1;
        int column = 2;

        var square = new Square(row, column);
        Assert.AreEqual(row, square.Row);
        Assert.AreEqual(column, square.Column);

    }
}
