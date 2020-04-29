using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.BattleShip.Model.UnitTests
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void SquareConstructorCreatesSquareWithGivenPosition()
        {
            Square s = new Square(1, 8);
            Assert.AreEqual(1, s.row);
            Assert.AreEqual(8, s.column);
        }

        [TestMethod]
        public void WhenShipIsSunkAllSquaresAreMarkedSunk()
        {
            throw new NotImplementedException();
        }
    }
}
