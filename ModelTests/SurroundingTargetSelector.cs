using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SurroundingTargetSelector
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var grid = new ShotsGrid(10 , 10);
            var squareHit= grid.Squares.FirstOrDefault(s => s.Row == 3 && s.Column == 4);
            squareHit.ChangeState(SquareState.Hit);
            int shipLength = 5;
            var selector = new SurroundingTargetSelector(grid, squareHit, shipLength);
        }
    }
}