using Model;

namespace ModelTests;

[TestClass]
public class GridTests
{
    [TestMethod]
    public void ConstructorCreateGridWithEnumerableSquareTest()
    {
        int row = 1;
        int column = 2;

        Grid grid = new Grid(row, column);

        Assert.AreEqual(grid.Squares.Count(), 2);
    }
}
