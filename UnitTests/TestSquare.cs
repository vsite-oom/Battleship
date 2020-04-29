using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void SquareConstructorCreatesSquareWithGivenPosition()
        {
            Square s = new Square(1, 8);
            Assert.AreEqual(1, s.Row);
            Assert.AreEqual(8, s.Column);
        }
        
        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAremarkedSunken()
        {
            //Ship ship = new Ship(); Ship 3 polja, pogoditi, i provjera je su li svi squarovi u stanju sunken
            throw new NotImplementedException();
        }
    }
}
