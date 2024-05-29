using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SurroundingTargetSelectorTests
    {
        //private IEnumerable<Square> CreateCandidates(ShotsGrid grid, IEnumerable<SquareCoordinate> coord)
        //{
        //    List<Square> result = new List<Square>();
        //    foreach (var c in coord)
        //    {
        //        var square = grid.Squares.FirstOrDefault(s => s.Row == c.Row && s.Column == c.Column);
        //        result.Add(square!);
        //    }
        //    return result;
        //}

        [TestMethod]
        public void NextReturnsOneOfFourSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            var target = selector.Next();

            var candidates = new SquareCoordinate[]{ new(2, 4), new(3, 5), new(4, 4), new(3, 3)};

            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
        }

        [TestMethod]
        public void NextReturnsOneOfThreeSquaresAroundSquare3x4AfterSquare3x5IsMissed()
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
        public void NextReturnsOneOfTwoSquaresAroundSquare3x4AfterSquares3x5And3x3AreMissed()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            grid.ChangeSquareState(3, 5, SquareState.Missed);
            grid.ChangeSquareState(3, 3, SquareState.Missed);

            var target = selector.Next();

            var candidates = new SquareCoordinate[] { new(2, 4), new(4, 4) };

            Assert.IsTrue(candidates.Contains(new SquareCoordinate(target.Row, target.Column)));
        }

        [TestMethod]
        public void NextReturnsTheOnlyOneSquareAroundSquare3x4AfterSquares3x5And3x3And2x4AreMissed()
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
