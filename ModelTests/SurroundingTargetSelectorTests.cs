using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using System;
using vsite.oom.battleship.model;

namespace Vsite.Oom.Battleship.Model.Tests
{
    

    [TestClass]
    public class SurroundingTargetSelectorTests
    {
        private IEnumerable<Square> CreateCandidates(ShotsGrid grid, IEnumerable<SquareCoordinate> coord)
        {
            List<Square> result = new List<Square>();
            foreach (var c in coord)
            {
                var square = grid.Squares.FirstOrDefault(s => s.Row == c.Row && s.Column == c.Column);
                result.Add(square!);
            }
            return result;
        }

        [TestMethod]
        public void OneOfSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            var target = selector.Next();
            var candidates = CreateCandidates(grid, new List<SquareCoordinate> { new SquareCoordinate(2, 4), new SquareCoordinate(3, 5), new SquareCoordinate(4, 4), new SquareCoordinate(3, 3) });
            Assert.IsTrue(candidates.Contains(target));
        }
    }
}