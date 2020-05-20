using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestSortedSquares
    {
        [TestMethod]
        public void AfterNewSquareIsAddedHorizontallySquaresAreSortedByColumns()
        {
            SortedSquares s = new SortedSquares();
            s.Add(new Square(3, 4));
            s.Add(new Square(3, 3));
            Assert.AreEqual(new Square(3, 3), s.First());
            s.Add(new Square(3, 5));
            Assert.AreEqual(new Square(3, 5), s.Last());

        }
        [TestMethod]
        public void AfterNewSquareIsAddedVerticallySquaresAreSortedByRows()
        {
            SortedSquares s = new SortedSquares();
            s.Add(new Square(4, 4));
            s.Add(new Square(3, 4));
            Assert.AreEqual(new Square(3, 4), s.First());
            s.Add(new Square(5, 4));
            Assert.AreEqual(new Square(5, 4), s.Last());

        }
    }
}
