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
            var grid = new Grid(rows, columns);
            Assert.AreEqual(rows * columns, grid.AvalibleSquares.Count());
        }
        [TestMethod]
        public void GetAvalibleSequancesReturns2SequancesOfLenght3ForGridWith1Row4Colums()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(3, result.ElementAt(0).Count());
            Assert.AreEqual(3, result.ElementAt(1).Count());

        } [TestMethod]
        public void GetAvalibleSequancesReturns3SequancesOfLenght3ForGridWith5Row1Colums()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(3, result.ElementAt(0).Count());
            Assert.AreEqual(3, result.ElementAt(1).Count());
            Assert.AreEqual(3, result.ElementAt(2).Count());

        }

    }
}

