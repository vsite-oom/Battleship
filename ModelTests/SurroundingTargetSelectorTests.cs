using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SurrondingTargetSelectorTests
    {
        private IEnumerable<Square> CreateCanditates(ShotsGrid grid, IEnumerable<SquareCoordinate> coords)
        {
            List<Square> result = new List<Square>();
            foreach (var c in coords)
            {
                var square = grid.Squares.FirstOrDefault(s => s.Row == c.Row && s.Column == c.Column);
                result.Add(square!);
            }
            return result;
        }

        [TestMethod]
        public void NextReturnsOneOfSquaresAroundSquare3x4()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLen = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLen);
            var target = selector.Next();
            var candidates = CreateCanditates(grid, [new(2, 4), new(3, 5), new(4, 4), new(3, 3)]);
            Assert.IsTrue(candidates.Contains(target));
        }
    }
}
