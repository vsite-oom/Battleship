using Model;

namespace ModelTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void Constructor_CreatesAllSquares()
        {
            var grid = new FleetGrid(10, 10);
            Assert.AreEqual(100, grid.Squares.Count());
        }

        [TestMethod]
        public void Constructor_SquaresHaveCorrectPositions()
        {
            var grid = new FleetGrid(3, 4);
            var squares = grid.Squares.ToList();

            Assert.AreEqual(12, squares.Count);
            Assert.IsTrue(squares.Any(s => s.Position.Row == 0 && s.Position.Column == 0));
            Assert.IsTrue(squares.Any(s => s.Position.Row == 2 && s.Position.Column == 3));
        }

        [TestMethod]
        public void GetAvailablePlacements_HorizontalOnSingleRow()
        {
            var grid = new FleetGrid(1, 4);
            var placements = grid.GetAvailablePlacements(3).ToList();

            Assert.AreEqual(2, placements.Count);
        }

        [TestMethod]
        public void GetAvailablePlacements_VerticalOnSingleColumn()
        {
            var grid = new FleetGrid(5, 1);
            var placements = grid.GetAvailablePlacements(3).ToList();

            Assert.AreEqual(3, placements.Count);
        }

        [TestMethod]
        public void GetAvailablePlacements_FullGrid()
        {
            var grid = new FleetGrid(10, 10);
            var placements = grid.GetAvailablePlacements(5).ToList();

            Assert.AreEqual(120, placements.Count);
        }
    }
}
