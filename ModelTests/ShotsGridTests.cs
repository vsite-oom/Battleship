using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShotsGridTests
    {
        [TestMethod]
        public void GetSquaresInDirectionGetSquaresInDirectionReturns3SquaresAboveSquare3x3()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 3;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Upwards);
            Assert.AreEqual(3, squares.Count());
        }
    
        [TestMethod]
        public void GetSquaresInDirectionGetSquaresInDirectionReturns4SquaresRightFromSquare3x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 5;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Rightwards);
            Assert.AreEqual(4, squares.Count());
        }

        [TestMethod]
        public void GetSquaresInDirectionGetSquaresInDirectionReturns2SquaresBelowFromSquare7x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 5;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Downwards);
            Assert.AreEqual(2, squares.Count());
        }

        [TestMethod]
        public void GetSquaresInDirectionGetSquaresInDirectionReturns1SquaresLeftFromSquare7x1()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 1;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Leftwards);
            Assert.AreEqual(1, squares.Count());
        }
    }
}