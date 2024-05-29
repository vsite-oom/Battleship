using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class SurrondingTargetSelectorTests
    {
        [TestMethod]
        public void NextReturnsOneOfSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit= grid.Squares.FirstOrDefault(s=>s.Row == 3 && s.Column==4);
            squareHit!.changeState(SquareState.Hit);
            int shipLen = 5;
            var selector =new SurrondingTargetSelector(grid, squareHit, shipLen);
            var target = selector.Next();
            var candidates = new SquareCoordinate[]{ new(2, 4), new(3, 5), new(4, 4), new(3, 3)};
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row,target.Column)));
        }
        [TestMethod]
        public void NextReturnsOneOfThreeSquaresAroundSquare3x4AfterSquareIsMisseed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.changeState(SquareState.Hit);
            int shipLen = 5;
            var selector = new SurrondingTargetSelector(grid, squareHit, shipLen);
            grid.ChangeSquareState(3, 5, SquareState.Miss);
            var target = selector.Next();
            var candidates = new SquareCoordinate[] { new(2, 4), new(4, 4), new(3, 3) };
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(3, 5)));
        }
        [TestMethod]
        public void NextReturnsOneOfTwoSquaresAroundSquare3x4AfterSquaresWereMisseed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.changeState(SquareState.Hit);
            int shipLen = 5;
            var selector = new SurrondingTargetSelector(grid, squareHit, shipLen);
            grid.ChangeSquareState(3, 5, SquareState.Miss);
            grid.ChangeSquareState(3, 3, SquareState.Miss);
            var target = selector.Next();
            var candidates = new SquareCoordinate[] { new(2, 4), new(4, 4) };
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(3, 5)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(3, 3)));
        }
        [TestMethod]
        public void NextReturnsOnlySquareAroundSquare3x4AfterOtherSquaresWereMisseed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.changeState(SquareState.Hit);
            int shipLen = 5;
            var selector = new SurrondingTargetSelector(grid, squareHit, shipLen);
            grid.ChangeSquareState(3, 5, SquareState.Miss);
            grid.ChangeSquareState(3, 3, SquareState.Miss);
            grid.ChangeSquareState(2, 4, SquareState.Miss);
            var target = selector.Next();
            var candidates = new SquareCoordinate[] {new(4, 4) };
            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(3, 5)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(3, 3)));
            Assert.IsFalse(candidates.Contains(new SquareCoordinate(2, 4)));
        }
    }
}