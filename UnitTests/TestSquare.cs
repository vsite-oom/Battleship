using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            List<Square> squares = new List<Square>()
            {
                new Square(1,0),
                new Square(2,0),
                new Square(3,0)
            };

            Ship ship = new Ship(squares);

            foreach (Square square in ship.Squares)
            {
                square.SetState(HitResult.Sunken);
            }


            foreach (Square square in ship.Squares)
            {
                Assert.AreEqual(SquareState.Sunken, square.SquareState);
            }


        }

    }
}
