using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.BattleShip.Model.UnitTests
{
    [TestClass]
    public class TestSortedSquares
    {
        [TestMethod]
        public void AfterNewSquareIsAddedHorizontallySquaresAreSortedByColumn()
        {
            SortedSquares sq = new SortedSquares();
            sq.Add(new Square(3, 3));
            sq.Add(new Square(3, 4));
            sq.Add(new Square(3, 5));
            Assert.AreEqual(new Square(3, 3), sq.First());
            Assert.AreEqual(new Square(3, 5), sq.Last());
        }

        [TestMethod]
        public void AfterNewSquareIsAddedVerticallySquaresAreSortedByRow()
        {
            SortedSquares sq = new SortedSquares();
            sq.Add(new Square(4, 3));
            sq.Add(new Square(5, 3));
            sq.Add(new Square(6, 3));
            Assert.AreEqual(new Square(4, 3), sq.First());
            Assert.AreEqual(new Square(6, 3), sq.Last());
        }
    }
}
