using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Model;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSortedSquares
    {
        [TestMethod]
        public void AfterNewSquareIsAddedHorizontallySquaresAreSortedByColumn()
        {
            SortedSquares sortedSquares = new SortedSquares();
            sortedSquares.Add(new Square(3, 4));
            sortedSquares.Add(new Square(3, 3));

            Assert.AreEqual(new Square(3, 3), sortedSquares.First());
            sortedSquares.Add(new Square(3, 5));
            Assert.AreEqual(new Square(3,5), sortedSquares.Last());


        }
        [TestMethod]
        public void AfterNewSquareIsAddedVerticallySquaresAreSortedByRow()
        {
            SortedSquares sortedSquares = new SortedSquares();
            sortedSquares.Add(new Square(4, 4));
            sortedSquares.Add(new Square(3, 4));

            Assert.AreEqual(new Square(3, 4), sortedSquares.First());
            sortedSquares.Add(new Square(5, 4));
            Assert.AreEqual(new Square(5, 4), sortedSquares.Last());


        }
    }
}
