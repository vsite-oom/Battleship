using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class GridTests
    {

        [TestMethod]
        public void ConstructorCreatesGridWithGivenNumberOfSquares()
        {
            int rows = 10;
            int columns = 10;
            var grid = new FleetGrid(10, 10);
            Assert.AreEqual(rows * columns, grid.AvailableSquares().Count());
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsTwoSquencesOfLenght3ForGrid1Row4Columns()
        {
            var grid = new FleetGrid(1, 4);
            var results = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, results.Count());

            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 0))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(0, 1))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(0, 2))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 3))));

        }

        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght3ForGrid5Row1Columns()
        {
            var grid = new FleetGrid(5, 1);
            var results = grid.GetAvailableSequences(3);
            Assert.AreEqual(3, results.Count());

            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 0))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(1, 0))));
            Assert.AreEqual(3, results.Count(s => s.Contains(new Square(2, 0))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(3, 0))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(4, 0))));

        }

        [TestMethod]
        public void GetAvailableSquencesReturnsThreeSquencesOfLenght2ForGrid1Row6Columns0_2IsRemoved()
        {
            int rows = 1;
            int columns = 6;
            var grid = new FleetGrid(1, 6);
            grid.RemoveSquare(0, 2);
            var results = grid.GetAvailableSequences(2);
            Assert.AreEqual(3, results.Count());

            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 0))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 1))));
            Assert.AreEqual(0, results.Count(s => s.Contains(new Square(0, 2))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 3))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(0, 4))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(0, 5))));
        }

        [TestMethod]
        public void GetAvailableSquencesReturnsTwoSquencesOfLenght3ForGrid5Row1Columns1_0IsRemoved()
        {
            var grid = new FleetGrid(5, 1);
            grid.RemoveSquare(1, 0);
            var results = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, results.Count());

            Assert.AreEqual(0, results.Count(s => s.Contains(new Square(0, 0))));
            Assert.AreEqual(0, results.Count(s => s.Contains(new Square(1, 0))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(2, 0))));
            Assert.AreEqual(2, results.Count(s => s.Contains(new Square(3, 0))));
            Assert.AreEqual(1, results.Count(s => s.Contains(new Square(4, 0))));
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedMissed()
        {
            var grid = new RecordGrid(5, 1);
            grid.MarkSquare(1, 0, HitResult.Missed);
            var result = grid.GetAvailableSequences(2);

            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedHit()
        {
            var grid = new RecordGrid(5, 1);
            grid.MarkSquare(1, 0, HitResult.Hit);
            var result = grid.GetAvailableSequences(2);

            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedSank()
        {
            var grid = new RecordGrid(5, 1);
            grid.MarkSquare(1, 0, HitResult.Sank);
            var result = grid.GetAvailableSequences(2);

            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsThreeSquaresLeftFromSquare3_3()
        {
            var grid = new RecordGrid(10, 10);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Leftwards);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(new Square(3, 0)));
            Assert.IsTrue(result.Contains(new Square(3, 1)));
            Assert.IsTrue(result.Contains(new Square(3, 2)));
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsThreeSquaresUpFromSquare3_3()
        {
            var grid = new RecordGrid(10, 10);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Upwards);

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(new Square(0, 3)));
            Assert.IsTrue(result.Contains(new Square(1, 3)));
            Assert.IsTrue(result.Contains(new Square(2, 3)));
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsSixSquaresRightFromSquare3_3()
        {
            var grid = new RecordGrid(10, 10);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Rightwards);

            Assert.AreEqual(6, result.Count());
            Assert.IsTrue(result.Contains(new Square(3, 4)));
            Assert.IsTrue(result.Contains(new Square(3, 5)));
            Assert.IsTrue(result.Contains(new Square(3, 9)));
            Assert.IsTrue(result.Contains(new Square(3, 9)));
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsSixSquaresDownFromSquare3_3()
        {
            var grid = new RecordGrid(10, 10);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Downwards);

            Assert.AreEqual(6, result.Count());
            Assert.IsTrue(result.Contains(new Square(4, 3)));
            Assert.IsTrue(result.Contains(new Square(5, 3)));
            Assert.IsTrue(result.Contains(new Square(9, 3)));
        }
    }
}
