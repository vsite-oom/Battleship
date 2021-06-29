using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSurroundingShooting
    {
        [TestMethod]
        public void NextTargetSelectsOneOfSquaresSurroundingSquare3_3()
        {
            Grid grid = new Grid(10, 10);
            Square square = new Square(3, 3);
            var shooting = new SurroundingShooting(grid, square, 3);
            var nextTarget = shooting.NextTarget();
            Assert.IsTrue(nextTarget.Equals(new Square(2, 3)) || nextTarget.Equals(new Square(3, 4))
                || nextTarget.Equals(new Square(4, 3)) || nextTarget.Equals(new Square(3, 2)));
        }
    }
}