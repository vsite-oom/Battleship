using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestLinearShooting
    {
        [TestMethod]
        public void NextTargetReturnsOneOfSquaresAboveOrBelowOfSquares3_3And4_3()
        {
            Grid grid = new Grid(10, 10);
            List<Square> squares = new List<Square> { new Square(3, 3), new Square(4, 3) };
            var shooting = new LinearShooting(grid, squares, 3);
            Assert.IsTrue(shooting.NextTarget().Equals(new Square(2, 3)) || shooting.NextTarget().Equals(new Square(5, 3)));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSquaresLeftOrRightOfSquares3_3And3_4()
        {
            Grid grid = new Grid(10, 10);
            List<Square> squares = new List<Square> { new Square(3, 3), new Square(3, 4) };
            var shooting = new LinearShooting(grid, squares, 3);
            Assert.IsTrue(shooting.NextTarget().Equals(new Square(3, 2)) || shooting.NextTarget().Equals(new Square(3, 5)));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSquareLeftOfSquares3_8And3_9()
        {
            Grid grid = new Grid(10, 10);
            List<Square> squares = new List<Square> { new Square(3, 8), new Square(3, 9) };
            var shooting = new LinearShooting(grid, squares, 3);
            Assert.IsTrue(shooting.NextTarget().Equals(new Square(3, 7)));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSquareRightOfSquares3_0And3_1()
        {
            Grid grid = new Grid(10, 10);
            List<Square> squares = new List<Square> { new Square(3, 0), new Square(3, 1) };
            var shooting = new LinearShooting(grid, squares, 3);
            Assert.IsTrue(shooting.NextTarget().Equals(new Square(3, 2)));
        }
    }
}
