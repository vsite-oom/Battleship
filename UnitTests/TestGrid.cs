using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void GetSequencesReturns100ArraysForAShipOfLength1SquareOnGridWith10Rows10Columns()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetSequences(1);
            Assert.AreEqual(100, placements.Count());
        }

        [TestMethod]
        public void GetSequencesReturns2ArraysForAShipOfLength3SquaresOnGridWith1Row4Columns()
        {
            Grid grid = new Grid(1, 4);
            var placements = grid.GetSequences(3);
            Assert.AreEqual(2, placements.Count());
            Assert.AreEqual(3, placements.ElementAt(0).Count());
            Assert.AreEqual(3, placements.ElementAt(1).Count());
        }

        [TestMethod]
        public void GetSequencesReturns3ArraysForAShipOfLength3SquaresOnGridWith5Rows1Column()
        {
            Grid grid = new Grid(5, 1);
            var placements = grid.GetSequences(3);
            Assert.AreEqual(3, placements.Count());
            Assert.AreEqual(3, placements.ElementAt(0).Count());
            Assert.AreEqual(3, placements.ElementAt(1).Count());
            Assert.AreEqual(3, placements.ElementAt(2).Count());
        }

        [TestMethod]
        public void GetSequencesReturns3ArraysForAShipOfLength2SquaresOnGridWith1Row6ColumnsAndSquare0_2Eliminated()
        {
            Grid grid = new Grid(1, 6);
            grid.RemoveSquares(new List<Square> { new Square(0, 2) });
            var placements = grid.GetSequences(2);
            Assert.AreEqual(3, placements.Count());
            Assert.AreEqual(2, placements.ElementAt(0).Count());
            Assert.AreEqual(2, placements.ElementAt(1).Count());
            Assert.AreEqual(2, placements.ElementAt(2).Count());
        }

        [TestMethod]
        public void GetSequencesReturns2ArraysForAShipOfLength2SquaresOnGridWith5Rows1ColumnAndSquare1_0Eliminated()
        {
            Grid grid = new Grid(5, 1);
            grid.RemoveSquares(new List<Square> { new Square(1, 0) });
            var placements = grid.GetSequences(2);
            Assert.AreEqual(2, placements.Count());
            Assert.AreEqual(2, placements.ElementAt(0).Count());
            Assert.AreEqual(2, placements.ElementAt(1).Count());
        }

        [TestMethod]
        public void GetSequenceReturnsArrayWithOneSquareOnlyAboveSquare1_1()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetSequence(new Square(1, 1), Direction.Up);
            Assert.AreEqual(1, placements.Count());
        }

        [TestMethod]
        public void GetSequenceReturnsArrayWithTwoSquaresLeftToSquare1_2()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetSequence(new Square(1, 2), Direction.Left);
            Assert.AreEqual(2, placements.Count());
        }

        [TestMethod]
        public void GetSequenceReturnsArrayWith8SquaresBelowSquare1_1()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetSequence(new Square(1, 1), Direction.Down);
            Assert.AreEqual(8, placements.Count());
        }

        [TestMethod]
        public void GetSequenceReturnsArrayWith7SquaresRightToSquare1_2()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetSequence(new Square(1, 2), Direction.Right);
            Assert.AreEqual(7, placements.Count());
        }
    }
}
