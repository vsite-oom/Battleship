﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class FleetBuilderTests
    {
        [TestMethod]
        public void CreateFleetBuildsFleetWithNumberOfShipsProvided()
        {
            int rows = 10;
            int columns = 10;
            int[] shipLenghts = {2,2,2,2,3,3,3,4,4,5};

            var builder = new FleetBuilder(rows, columns, shipLenghts);
            var fleet = builder.CreateFleet();

            Assert.AreEqual(10, fleet.Ships.Count());
        }
        public void CreateFleetBuildsFleetWithShipsOfLengthProvided()
        {
            int rows = 10;
            int columns = 10;
            int[] shipLenghts = {2,2,2,2,3,3,3,4,4,5};

            var builder = new FleetBuilder(rows, columns, shipLenghts);
            var fleet = builder.CreateFleet();

            Assert.AreEqual(10, fleet.Ships.Count(s=>s.Squares.Count()==2));
        }
    }
}