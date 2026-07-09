using Model;

namespace ModelTests
{
    [TestClass]
    public class ShotsGridTests
    {
        [TestMethod]
        public void Constructor_AllSquaresAvailable()
        {
            var grid = new ShotsGrid(10, 10);
            Assert.AreEqual(100, grid.Squares.Count());
        }

        [TestMethod]
        public void ChangeSquareState_MarksSquare()
        {
            var grid = new ShotsGrid(10, 10);
            grid.ChangeSquareState(0, 0, SquareState.Missed);

            var square = grid.Squares.First(s => s.Position.Row == 0 && s.Position.Column == 0);
            Assert.AreEqual(SquareState.Missed, square.State);
        }

        [TestMethod]
        public void GetAvailablePlacements_ExcludesNonNoneSquares()
        {
            var grid = new ShotsGrid(1, 4);
            grid.ChangeSquareState(0, 1, SquareState.Missed);

            var placements = grid.GetAvailablePlacements(3).ToList();
            Assert.AreEqual(0, placements.Count);
        }
    }
}
