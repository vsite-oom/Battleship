using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ConstructorCreatesGridWith50SquaresFor5RowsAnd10Columns()
        {
            int rows = 5;
            int columns = 10;
            Grid grid = new Grid(rows, columns);
            Assert.AreEqual(50, grid.Squares.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid1x5Returns3PlacementsForShipWith3Squares()
        {
            int rows = 1;
            int columns = 5;
            int shipLength = 3;
            Grid grid = new(rows, columns);

            Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid7x1Returns6PlacementsForShipWith2Squares()
        {
            int rows = 7;
            int columns = 1;
            int shipLength = 2;
            Grid grid = new(rows, columns);

            Assert.AreEqual(6, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid5x5Returns20PlacementsForShipWith4Squares()
        {
            int rows = 5;
            int columns = 5;
            int shipLength = 4;
            Grid grid = new(rows, columns);

            Assert.AreEqual(20, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid1x6Returns3PlacementsForShipWith2SquaresAfterSquareInColumn3IsEliminated()
        {
            int rows = 1;
            int columns = 6;
            int shipLength = 2;
            Grid grid = new(rows, columns);

            grid.EliminateSquare(0, 3);

            Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForGrid8x1Returns3PlacementsForShipWith2SquaresAfterSquaresInRows3And5AreEliminated()
        {
            int rows = 8;
            int columns = 1;
            int shipLength = 2;
            Grid grid = new(rows, columns);

            grid.EliminateSquare(3, 0);
            grid.EliminateSquare(5, 0);

            Assert.AreEqual(3, grid.GetAvailablePlacements(shipLength).Count());
        }
    }
}