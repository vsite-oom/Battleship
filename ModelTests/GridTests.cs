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

            Assert.AreEqual(1, result.Count(seq => seq.Contains(new Square(0,0))));
            Assert.AreEqual(2, result.Count(seq => seq.Contains(new Square(0,1))));
            Assert.AreEqual(2, result.Count(seq => seq.Contains(new Square(0,2))));
            Assert.AreEqual(1, result.Count(seq => seq.Contains(new Square(0,3))));

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
        [TestMethod]
        public void GetAvalibleSequancesReturns3SequancesOfLenght2ForGridWith1Row6ColumsAfterSquare0_2IsRemoved()
        {
            int rows = 1;
            int columns = 6;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(0, 2);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(2, result.ElementAt(0).Count());
            Assert.AreEqual(2, result.ElementAt(1).Count());
            Assert.AreEqual(2, result.ElementAt(2).Count());

        } [TestMethod]
        public void GetAvalibleSequancesReturns2SequancesOfLenght2ForGridWith5Row1ColumsAfterSquare1_0IsRemoved()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(1, 0);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result.ElementAt(0).Count());
            Assert.AreEqual(2, result.ElementAt(1).Count());

        }

    }
}

