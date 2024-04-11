using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquareWithRowAndColumnProvided()
        {
            int row = 4;
            int column = 8;
            var square = new Square(row, column);

            Assert.AreEqual(row, square.Row);
            Assert.AreEqual(column, square.Column);
        }
    }
}