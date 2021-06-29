using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void SquareReturnsItsRowAndColumn()
        {
            Square sq = new Square(2, 3);
            Assert.AreEqual(2, sq.Row);
            Assert.AreEqual(3, sq.Column);
        }
    }
}
