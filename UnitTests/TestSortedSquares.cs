using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSortedSquares
    {
        [TestMethod]
        public void AfterNewSquareIsAddedHorizontallySquaresAreSortedByColumn()
        {
            var ss = new SortedSquares();
            ss.Add(new Square(3, 4));
            ss.Add(new Square(3, 3));
            Assert.AreEqual(new Square(3, 3), ss.First());

            ss.Add(new Square(3, 5));
            Assert.AreEqual(new Square(3, 5), ss.Last());
        }

        [TestMethod]
        public void AfterNewSquareIsAddedVerticallySquaresAreSortedByRow()
        {
            var ss = new SortedSquares();
            ss.Add(new Square(4, 4));
            ss.Add(new Square(3, 4));
            Assert.AreEqual(new Square(3, 4), ss.First());

            ss.Add(new Square(5, 4));
            Assert.AreEqual(new Square(5, 4), ss.Last());
        }
    }
}
