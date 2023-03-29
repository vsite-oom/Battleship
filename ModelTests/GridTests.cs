﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsie.Oom.Battleship.Model;

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
            Assert.AreEqual(rows * columns, grid.AvaliableSquares().Count());
        }
        [TestMethod]
        public void GetAvailableSequancesReturnsTwoSequancesOfLength3FarGridlRow4Columns()
        {
            int rows = 1;
            int columns = 4;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(2, result.Count());
        }
        [TestMethod]
        public void GetAvailableSequancesReturnsThreeSequancesOfLength5FarGridlRow1Columns()
        {
            int rows = 5;
            int columns = 1;
            var grid = new Grid(rows, columns);
            var result = grid.GetAvailableSequences(3);
            Assert.AreEqual(3, result.Count());
        }

    }
}
