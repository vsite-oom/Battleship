using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var grid = new Grid(rows, columns);
            Assert.AreEqual(rows * columns, grid.AvailableSquares().Count());
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength3ForGrid1Row4Columns()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(1, result.Count(s => s.Contains(new Square(0, 0))));
            Assert.AreEqual(2, result.Count(s => s.Contains(new Square(0, 1))));
            Assert.AreEqual(2, result.Count(s => s.Contains(new Square(0, 2))));
            Assert.AreEqual(1, result.Count(s => s.Contains(new Square(0, 3))));
        }
        
        [TestMethod]
        public void GetAvailableSequencesReturnsThreeSequencesOfLength3ForGrid5Rows1Column()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsThreeSequencesOfLength2ForGrid1Row6ColumnsAfterSquare0_2IsRemoved()
        {
            int rows = 1;
            int columns = 6;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(0, 2);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsRemoved()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(1, 0);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedMissed()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.MarkSquare(1, 0, HitResult.Missed);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedHit()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.MarkSquare(1, 0, HitResult.Hit);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsTwoSequencesOfLength2ForGrid5Rows1ColumnAfterSquare1_0IsMarkedSunk()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.MarkSquare(1, 0, HitResult.Sunk);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
            Assert.IsFalse(result.SelectMany(s => s).Contains(new Square(1, 0)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsThreeSquaresLeftFromSquare3_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Leftwards);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(new Square(3, 0)));
            Assert.IsTrue(result.Contains(new Square(3, 1)));
            Assert.IsTrue(result.Contains(new Square(3, 2)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsThreeSquaresUpFromSquare3_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Upwards);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(new Square(0, 3)));
            Assert.IsTrue(result.Contains(new Square(1, 3)));
            Assert.IsTrue(result.Contains(new Square(2, 3)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsSixSquaresRightFromSquare3_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Rightwards);
            Assert.AreEqual(6, result.Count());
            Assert.IsTrue(result.Contains(new Square(3, 4)));
            Assert.IsTrue(result.Contains(new Square(3, 5)));
            Assert.IsTrue(result.Contains(new Square(3, 9)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsSixSquaresDownFromSquare3_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 3), Direction.Downwards);
            Assert.AreEqual(6, result.Count());
            Assert.IsTrue(result.Contains(new Square(4, 3)));
            Assert.IsTrue(result.Contains(new Square(5, 3)));
            Assert.IsTrue(result.Contains(new Square(9, 3)));
        }

        [TestMethod]
        public void GetAvailableSequenceReturnsZeroSquaresLeftFromSquare3_0()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 0), Direction.Leftwards);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsZeroSquaresUpFromSquare0_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(0, 3), Direction.Upwards);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsZeroSquaresRightFromSquare3_9()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(3, 9), Direction.Rightwards);
            Assert.AreEqual(0, result.Count());
        }
        [TestMethod]
        public void GetAvailableSequenceReturnsZeroSquaresDownFromSquare9_3()
        {
            int rows = 10;
            int columns = 10;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequence(new Square(9, 3), Direction.Downwards);
            Assert.AreEqual(0, result.Count());
        }
    }
}