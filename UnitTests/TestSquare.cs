using System;
using System.Collections.Generic;
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

            List<int> ships = new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
            Shipwright shipWrighter = new Shipwright(10, 10);

            Fleet fleet = shipWrighter.CreateFleet(ships);

            foreach (var ship in fleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    ship.Hit(square);
                }
            }
            foreach (var ship in fleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    Assert.AreEqual(SquareState.Sunken, square.SquareState);
                }
            }
        }
    }
}
