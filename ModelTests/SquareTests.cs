
using Model;

namespace ModelTests;

[TestClass]
public class SquareTests
{
    [TestMethod]
    public void ConstructorCreateSquareWithRowAndColumnProvided()
    {
        int row = 1;
        int column = 2;

        Square square = new Square(row, column);

        Assert.AreEqual(row, square.Row);
        Assert.AreEqual(column, square.Column);
    }
}
