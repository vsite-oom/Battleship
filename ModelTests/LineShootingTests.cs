using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class LineShootingTests
    {
        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForSquares5_6and5_7()
        {
            Square[] hitSquares = { new Square(5, 6), new Square(5, 7) };
            var shooting = new LineShooting(new RecordGrid(10, 10), hitSquares, new int[] { 5 });
            var result = shooting.NextTarget();
            Square[] possibleResults = { new Square(5, 5), new Square(5, 8) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForSquares5_7and5_6()
        {
            Square[] hitSquares = { new Square(5, 7), new Square(5, 6) };
            var shooting = new LineShooting(new RecordGrid(10, 10), hitSquares, new int[] { 5 });
            var result = shooting.NextTarget();
            Square[] possibleResults = { new Square(5, 5), new Square(5, 8) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForSquares5_6and6_6()
        {
            Square[] hitSquares = { new Square(5, 6), new Square(6, 6) };
            var shooting = new LineShooting(new RecordGrid(10, 10), hitSquares, new int[] { 5 });
            var result = shooting.NextTarget();
            Square[] possibleResults = { new Square(4, 6), new Square(7, 6) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForSquares6_6and5_6and7_6()
        {
            Square[] hitSquares = { new Square(6, 6), new Square(5, 6), new Square(7, 6) };
            var shooting = new LineShooting(new RecordGrid(10, 10), hitSquares, new int[] { 5 });
            var result = shooting.NextTarget();
            Square[] possibleResults = { new Square(4, 6), new Square(8, 6) };
            CollectionAssert.Contains(possibleResults, result);
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForSquares0_6and2_6and1_6()
        {
            Square[] hitSquares = { new Square(0, 6), new Square(2, 6), new Square(1, 6) };
            var shooting = new LineShooting(new RecordGrid(10, 10), hitSquares, new int[] { 5 });
            var result = shooting.NextTarget();
            Square[] possibleResults = { new Square(3, 6) };
            CollectionAssert.Contains(possibleResults, result);
        }
    }
}