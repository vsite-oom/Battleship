using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestSquare
    {
        [TestMethod]
        public void squareConstructorCreatesSquareWithGivenPosition()
        {
            Square S = new Square(1, 8);
            Assert.AreEqual(1,S.row);
            Assert.AreEqual(8, S.column);
        }
        [TestMethod]
        public void WhenShipIsSunkenAllSquaresAllMarkedSunken()
        {
            ship ship = new ship(new List<Square> { new Square(1, 1), new Square(1, 2), new Square(1, 3) });
            int x = 1;
            for (int i =0;i<3;++i)
            {
                ship.Hit(new Square(1,x));
                ++x;
            }
            Assert.AreEqual(SquareState.Sunken, ship.squares.First().SquareState);
        }
    }
    }

