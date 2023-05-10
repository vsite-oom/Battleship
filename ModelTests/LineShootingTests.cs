using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class LineShootingTests
    {
        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquares5_6and5_7()
        {
            Square[] hitSquares = { new Square(5,6), new Square(5,7) };
            Grid grid = new(10, 10);
            LineShooting lShooting = new(grid, hitSquares, new int[] { 5 });

            var result = lShooting.AddNextTarget();
            Square[] testData = { new Square(5, 5), new Square(5, 8)};

            CollectionAssert.Contains(testData, result);
        }

        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquares5_6and6_6()
        {
            Square[] hitSquares = { new Square(5, 7), new Square(5, 6) };
            Grid grid = new(10, 10);
            LineShooting lShooting = new(grid, hitSquares, new int[] { 5 });

            var result = lShooting.AddNextTarget();
            Square[] testData = { new Square(5, 5), new Square(5, 8) };

            CollectionAssert.Contains(testData, result);
        }
        
        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquares6_6and5_6and7_6()
        {
            Square[] hitSquares = { new Square(6, 6), new Square(5, 6), new Square(7, 6) };
            Grid grid = new(10, 10);
            LineShooting lShooting = new(grid, hitSquares, new int[] { 5 });

            var result = lShooting.AddNextTarget();
            Square[] testData = { new Square(4, 6), new Square(8, 6) };

            CollectionAssert.Contains(testData, result);
        }

        [TestMethod]
        public void NextTargetReturns1OfSurroundingSquaresForFirstSquares0_6and2_6and1_6()
        {
            Square[] hitSquares = { new Square(0, 6), new Square(2, 6), new Square(1, 6) };
            Grid grid = new(10, 10);
            LineShooting lShooting = new(grid, hitSquares, new int[] { 5 });

            var result = lShooting.AddNextTarget();
            Square[] testData = { new Square(3, 6) };

            CollectionAssert.Contains(testData, result);
        }
    }
}