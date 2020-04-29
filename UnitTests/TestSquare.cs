using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
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
        public void WhenShipIsSunkenAllSquaresAreMarkedSunken()
        {
            //kreacija broda s 3 Square-a (1,1 - 1,2 - 1,3)
            Ship testShip = new Ship(new Square[] {
                                                    new Square(1,1),
                                                    new Square(1,2),
                                                    new Square(1,3) });

            //gadanje svakog polja broda
            foreach (var square in testShip.Squares)
                testShip.Hit(square);
            //provjeravamo je li svakom square-u dodijeljen SquareState.Sunken
            foreach (var square in testShip.Squares)
                Assert.AreEqual(SquareState.Sunken, square.SquareState);


        }
    }
}
