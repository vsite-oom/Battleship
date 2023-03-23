using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWithNumberOfSquaresEqualRowsTimesColumns()
        {
            int rows = 10;
            int columns = 8;
            var grid = new Grid(rows,columns);
            Assert.AreEqual(rows * columns,grid.AvalibleSquares.Count());

        }
    }
}
