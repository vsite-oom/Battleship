using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void GetAvailablePlacementsReturn2SequancesForShip3SquaresLongOnGrid1x4()
        {
            Grid grid = new Grid(1, 4);
            var placement = grid.GetAvailablePlacements(3);
            Assert.AreEqual(2, placement.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturn3SequancesForShip3SquaresLongOnGrid5x1()
        {
            Grid grid = new Grid(5, 1);
            var placement = grid.GetAvailablePlacements(3);
            Assert.AreEqual(3, placement.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturn100SequancesForShip1SquaresLongOnGrid10x10()
        {
            Grid grid = new Grid(10, 10);
            var placement = grid.GetAvailablePlacements(1);
            Assert.AreEqual(100, placement.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturn3SequancesForShip2SquaresLongOnGrid1x6WithSquare0_2Eliminated()
        {
            Grid grid = new Grid(1, 6);
            grid.Eliminate(new List<Square> { new Square(0, 2) });
            var placement = grid.GetAvailablePlacements(2);
            Assert.AreEqual(3, placement.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturn3SequancesForShip2SquaresLongOnGrid5x1WithSquare1_0liminated()
        {
            Grid grid = new Grid(5, 1);
            grid.Eliminate(new List<Square> { new Square(1, 0) });
            var placement = grid.GetAvailablePlacements(2);
            Assert.AreEqual(2, placement.Count());
        }
    }
}