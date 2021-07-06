using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSurroundingShooting
    {
        [TestMethod]
        public void NextTargetSelectsOneOfSquaresSurroundingSquare3_3()
        {
            Grid grid = new Grid(10, 10);
            Square square = new Square(3, 3);
            var shooting = new SurroundingShooting(grid, square, 4);
            Assert.IsTrue(shooting.NextTarget().Equals(new Square(2, 3)) || shooting.NextTarget().Equals(new Square(3, 4))
                || shooting.NextTarget().Equals(new Square(4, 3)) || shooting.NextTarget().Equals(new Square(3, 2)));
        }
    }
}