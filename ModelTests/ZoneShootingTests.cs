using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class ZoneShootingTests
    {
        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquare4_4()
        {
            RecordGrid grid = new(10, 10);
            ZoneShooting zShooting = new(grid, new Square(4, 4), new int[] { 5 });

            var result = zShooting.AddNextTarget();
            Square[] testData = { new Square(3, 4), new Square(4, 3), new Square(4, 5), new Square(5, 4) };

            CollectionAssert.Contains(testData, result);
        }
        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquare9_0()
        {
            RecordGrid grid = new(10, 10);
            ZoneShooting zShooting = new(grid, new Square(9, 0), new int[] { 5 });

            var result = zShooting.AddNextTarget();
            Square[] testData = { new Square(8, 0), new Square(8, 1), new Square(9, 1) };

            CollectionAssert.Contains(testData, result);
        }
    }
    // dodati test za polje u kutu
}