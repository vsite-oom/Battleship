using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class ShotsGridTests
    {
        [TestMethod]
        public void GetSquaresInDirectionReturns3SquaresAboveSquare3x3()
        {
            var grid=new ShotsGrid(10, 10);
            int row = 3;
            int col = 3;
            var squares=grid.GetSquaresInDirection(row,col,Direction.Upwards);
            Assert.AreEqual(3,squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns4SquaresRightSquare3x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 5;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Right);
            Assert.AreEqual(4, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns2SquaresDownSquare7x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 5;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Downwards);
            Assert.AreEqual(2, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns1SquaresLeftSquare7x1()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 1;
            var squares = grid.GetSquaresInDirection(row, col, Direction.Left);
            Assert.AreEqual(1, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns1SquaresAboveSquare3x3()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 3;
            grid.ChangeSquareState(1, 3, SquareState.Miss);
            var squares = grid.GetSquaresInDirection(row, col, Direction.Upwards);
            Assert.AreEqual(1, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns0SquaresAboveSquare3x3()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 3;
            grid.ChangeSquareState(2, 3, SquareState.Miss);
            var squares = grid.GetSquaresInDirection(row, col, Direction.Upwards);
            Assert.AreEqual(0, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns2SquaresRightSquare3x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int col = 5;
            grid.ChangeSquareState(3, 8, SquareState.Miss);
            var squares = grid.GetSquaresInDirection(row, col, Direction.Right);
            Assert.AreEqual(2, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns1SquaresDownSquare7x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 5;
            grid.ChangeSquareState(9, 5, SquareState.Miss);
            var squares = grid.GetSquaresInDirection(row, col, Direction.Downwards);
            Assert.AreEqual(1, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns0SquaresLeftSquare7x1()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int col = 1;
            grid.ChangeSquareState(7, 0, SquareState.Miss);
            var squares = grid.GetSquaresInDirection(row, col, Direction.Left);
            Assert.AreEqual(0, squares.Count());
        }
    }
}