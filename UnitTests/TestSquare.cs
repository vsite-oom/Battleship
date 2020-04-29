using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void SquareConstructorsCreateSquareWithGivenPosition()
        {
            Square s = new Square(1, 8);
            Assert.AreEqual(1, s.Rows);
            Assert.AreEqual(8, s.Columns);
        }

        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            throw new NotImplementedException(); //TODO: domaca zadaca
        }
    }
}
