using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Serialization;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class SurrondingTargetSelectorTests
    {
        private IEnumerable<Square> CreateCanditates(ShotsGrid grid, IEnumerable<SquareCoordinate> coords)
        {
            List<Square> result = new List<Square>();
            foreach (var c in coords)
            {
                var square = grid.Squares.FirstOrDefault(s => s.Row == c.row && s.Column == c.col);
                result.Add(square!);
            }
            return result;
        }

        [TestMethod]
        public void NextReturnsOneOfSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit= grid.Squares.FirstOrDefault(s=>s.Row == 3 && s.Column==4);
            squareHit!.changeState(SquareState.Hit);
            int shipLen = 5;
            var selector =new SurrondingTargetSelector(grid, squareHit, shipLen);
            var target = selector.Next();
            var candidates = CreateCanditates(grid, [new(2, 4), new(3, 5), new(4, 4), new(3, 3)]);
            Assert.IsTrue(candidates.Contains(target));
        }
    }
}