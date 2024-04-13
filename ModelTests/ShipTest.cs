﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShipTest
    {
        [TestMethod]
        public void ConstructorCreatesShipWithProvided()
        {
            var squares = new List <Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            var ship = new Ship(squares);

            Assert.IsTrue(ship.Contains(1, 4));
        }
    }
}