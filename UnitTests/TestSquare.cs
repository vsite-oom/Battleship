using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests {
    [TestClass]
    public class TestSquare {
        [TestMethod]
        public void SquareConstructorCreateASquareAtGivenRowAndColumn() {
            Square s = new Square(5, 4);
            Assert.AreEqual(5, s.row);
            Assert.AreEqual(4, s.column);
        }

        [TestMethod]
        public void SquareSetSquareStateChangesSquareStateToMissedForHitResultMissed() {
            Square s = new Square(5, 4);
            Assert.AreEqual(SquareState.Default, s.SquareState);
            s.SetSquareState(HitResult.Missed);
            Assert.AreEqual(SquareState.Missed, s.SquareState);
        }

        [TestMethod]
        public void SquareSetSquareStateChangesSquareStateToHitForHitResultHit() {
            Square s = new Square(5, 4);
            Assert.AreEqual(SquareState.Default, s.SquareState);
            s.SetSquareState(HitResult.Hit);
            Assert.AreEqual(SquareState.Hit, s.SquareState);
        }

        [TestMethod]
        public void SquareSetSquareStateChangesSquareStateToSunkenForHitResultSunken() {
            Square s = new Square(5, 4);
            Assert.AreEqual(SquareState.Default, s.SquareState);
            s.SetSquareState(HitResult.Sunken);
            Assert.AreEqual(SquareState.Sunken, s.SquareState);
        }
    }
}