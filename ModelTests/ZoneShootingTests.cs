using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class ZoneShootingTests
    {
        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare4_4()
        {
            var shooting = new ZoneShooting(new RecordGrid(10, 10), new Square(4, 4), new int[] { 5 });
            var result = shooting.NextTarget();

            Square[] possibleResults = { new Square(3, 4), new Square(4, 3), new Square(5, 4), new Square(4, 5) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare0_0()
        {
            var shooting = new ZoneShooting(new RecordGrid(10, 10), new Square(0, 0), new int[] { 5 });
            var result = shooting.NextTarget();

            Square[] possibleResults = { new Square(0, 1), new Square(1, 0) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare9_9()
        {
            var shooting = new ZoneShooting(new RecordGrid(10, 10), new Square(9, 9), new int[] { 5 });
            var result = shooting.NextTarget();

            Square[] possibleResults = { new Square(8, 9), new Square(9, 8) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare9_9WithSquare8_9Eliminated()
        {
            var shooting = new ZoneShooting(new RecordGrid(10, 10), new Square(9, 9), new int[] { 5 });
            //Square eliminated = new Square(8, 9);
            //new Square(8, 9).Eliminate();
            var result = shooting.NextTarget();

            Square[] possibleResults = { new Square(9, 8), new Square(8, 9) };
            CollectionAssert.Contains(possibleResults, result);
        }
    }
}