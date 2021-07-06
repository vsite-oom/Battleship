using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingSquareConstructorsRowAndColumn()
        {
            Square testingSquare = new Square(5, 4);
            Assert.AreEqual(5, testingSquare.Row);
            Assert.AreEqual(4, testingSquare.Column);
        }

        [TestMethod]
        public void WhenSquareIsInitializedSquareStateIsDefault()
        {
            Square square = new Square(5, 4);
            Assert.AreEqual(SquareState.Default, square.SquareState);
        }


        [TestMethod]
        public void SetSquareStateChangesSquareStateToMissed()
        {
            Square square = new Square(5, 4);

            Assert.AreEqual(SquareState.Default, square.SquareState);
            square.SetSquareState(HitResult.Missed);
            Assert.AreEqual(SquareState.Missed, square.SquareState);
        }

        [TestMethod]
        public void SetSquareStateChangesSquareStateToHit()
        {
            Square square = new Square(5, 4);

            square.SetSquareState(HitResult.Hit);
            Assert.AreEqual(SquareState.Hit, square.SquareState);
        }

        [TestMethod]
        public void SetSquareStateChangesSquareStateToSunken()
        {
            Square square = new Square(5, 4);

            square.SetSquareState(HitResult.Sunken);
            Assert.AreEqual(SquareState.Sunken, square.SquareState);
        }
    }
}
