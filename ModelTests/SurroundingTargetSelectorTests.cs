using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SurroundingTargetSelectorTests
    {
        private IEnumerable<Square> CreateCandidates(ShotsGrid grid, IEnumerable<SquareCoordinates> coordinates)
        {
            List<Square> results = new List<Square>();
            foreach (var c in coordinates)
            {
                var square = grid.Squares.FirstOrDefault(s => s.Row == c.Row && s.Column == c.Column);
                results.Add(square!);
            }
            return results;
        }

        [TestMethod]
        public void NextReturnsOneOfSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10,10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            var tareget = selector.Next();

            var candidates = CreateCandidates(grid, [new(2, 4), new(3, 5), new(4, 4), new(3, 3)]);
            Assert.IsTrue(candidates.Contains(tareget));
        }
    }
}