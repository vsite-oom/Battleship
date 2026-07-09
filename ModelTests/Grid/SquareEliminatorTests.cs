using Model;

namespace ModelTests
{
    [TestClass]
    public class SquareEliminatorTests
    {
        [TestMethod]
        public void ToEliminate_ReturnsSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var ship = new[] { new Square(3, 3) };
            var result = eliminator.ToEliminate(ship, 10, 10).ToList();

            Assert.AreEqual(9, result.Count);
        }

        [TestMethod]
        public void ToEliminate_ClampsToGridBounds_Corner()
        {
            var eliminator = new SquareEliminator();
            var ship = new[] { new Square(0, 0) };
            var result = eliminator.ToEliminate(ship, 10, 10).ToList();

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void ToEliminate_NoDuplicates_ForAdjacentShipSquares()
        {
            var eliminator = new SquareEliminator();
            var ship = new[] { new Square(1, 1), new Square(1, 2) };
            var result = eliminator.ToEliminate(ship, 10, 10).ToList();

            Assert.AreEqual(result.Count, result.Distinct().Count());
        }
    }
}
