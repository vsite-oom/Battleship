using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void GetAvailablePlacementsReturns2SequencesForShip3SquaresLongOnGrid1x4()
        {
            Grid grid = new Grid(1, 4);
            var placements = grid.GetAvailablePlacements(3);
            Assert.AreEqual(2, placements.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturns3SequencesForShip3SquaresLongOnGrid5x1()
        {
            Grid grid = new Grid(5, 1);
            var placements = grid.GetAvailablePlacements(3);
            Assert.AreEqual(3, placements.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsReturns100SequencesForShip1SquaresLongOnGrid10x10()
        {
            Grid grid = new Grid(10, 10);
            var placements = grid.GetAvailablePlacements(1);
            Assert.AreEqual(100, placements.Count());
        }         
        
        [TestMethod]
        public void GetAvailablePlacementsReturns3SequencesForShip2SquaresLongOnGrid1x6WithSquare0_2Eliminated()
        {
            Grid grid = new Grid(1, 6);
            grid.Eliminate(new List<Square>{new Square(0,2) });
            var placements = grid.GetAvailablePlacements(2);
            Assert.AreEqual(3, placements.Count());
        }         
        
        [TestMethod]
        public void GetAvailablePlacementsReturns2SequencesForShip2SquaresLongOnGrid5x1WithSquare1_0Eliminated()
        {
            Grid grid = new Grid(5, 1);
            grid.Eliminate(new List<Square>{new Square(1,0) });
            var placements = grid.GetAvailablePlacements(2);
            Assert.AreEqual(2, placements.Count());
        }       

    }
}
