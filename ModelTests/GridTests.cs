﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void GetAvailableSequencesesReturnsTwoSequencesOfLength3ForGrid1Row4Columns()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetAvailableSequencesReturnsThreeSequencesOfLength3ForGrid5Row5Columns()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, result.Count());
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
        public void GetAvailableSequencesesReturnsTwoSequencesOfLength2ForGrid5Row1ColumnsAfterSquare1_0IsRemoved()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            grid.RemoveSquare(1, 0);
            var result = grid.GetAvailableSequences(2);
            Assert.AreEqual(2, result.Count());
        }
    }
}