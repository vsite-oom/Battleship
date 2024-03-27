using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void Constructor()
        {
            int row = 4;
            int col = 8;
            var square = new Square(4, 8);
            Assert.AreEqual(row, square.Row);
            Assert.AreEqual(col,square.Column);
        }
    }
}