using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    public class SurroundingTargetSelectorTests
    {
        private IEnumerable<Square> CreateCandidates(ShotsGrid grid, IEnumerable<SquareCoordinate> coord) 
    {
        List<Square> result = new List<Square>();
        foreach (var c in coord)
        {
            var square = grid.Squares.FirstOrDefault(S => S.Row == c.Row && S.Column == c.Column);
            result.Add(square);
        }
        return result;
    }
    [TestClass]
 
        [TestMethod]
        public void MyTestMethoNextReturnsOneOfSquareAroundSquare ()
        {
            var grid = new ShotsGrid(10, 10);
            var squareHit = grid.Squares.FirstOrDefault(S => S.Row == 3 && S.Column == 4);
            squareHit!.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
            var target = selector.Next();

            var candidates = CreateCandidates(grid, [new(2,4), new(3, 5), new(4, 4), new(3, 3)]);
            Assert.IsTrue(candidates.Contains(target));



        }
    }
}