using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SurroundingTargetSelectorTests
    {

        [TestMethod]
        public void NextReturnsOneOfFourSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
           
            var target = selector.Next();

            var candidates = new SquareCoordinate[] { new(2, 4), new(3, 5), new(4, 4), new(3, 3)};
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
        }

        [TestMethod]
        public void NextReturnsOneThreeSquaresAroundSquare3x5IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;

            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            grid.ChangeSquareState(3, 5, SquareState.Missed);
            
            var target = selector.Next();

            var candidates = new SquareCoordinate[] { new(2, 4), new(4, 4), new(3, 3) };
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
        }

        [TestMethod]
        public void NextReturnsOneTwoSquaresAroundSquare3x3areMissed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;

            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            grid.ChangeSquareState(3, 5, SquareState.Missed);
            grid.ChangeSquareState(3, 3, SquareState.Missed);

            var target = selector.Next();

            var candidates = new SquareCoordinate[] { new(2, 4), new(4, 4)};
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
        }

        [TestMethod]
        public void NextReturnsTheOnlySquaresAroundSquare3x3areMissed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;

            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            grid.ChangeSquareState(3, 5, SquareState.Missed);
            grid.ChangeSquareState(3, 3, SquareState.Missed);
            grid.ChangeSquareState(2, 4, SquareState.Missed);

            var target = selector.Next();

            Assert.IsTrue(target.Row == 4 && target.Column == 4);
        }
    }
}