using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareEliminatorTests
    {
        [TestMethod]
        public void ForSquares4x3To4x6Returns18SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4,6) };
            Assert.AreEqual(18, eliminator.ToEliminate(shipSquares, 10, 10).Count());
        }
        [TestMethod]
        public void ForSquares3x9To4x9Returns8SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(3, 9), new Square(4, 9) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(8, toEliminate.Count());
        }
        [TestMethod]
        public void ForSquares0x3To0x4Returns8SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(0, 3), new Square(0, 4) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(8, toEliminate.Count());
        }
        [TestMethod]
        public void ForSquares5x0To5x1Returns9SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(5, 0), new Square(5, 1) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(9, toEliminate.Count());
        }
        [TestMethod]
        public void ForSquares7x5To9x5Returns12SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(7, 5), new Square(8, 5), new Square(9, 5) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(12, toEliminate.Count());
        }
        [TestMethod]
        public void ForSquares0x0To0x1Returns6SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(0, 0), new Square(0, 1) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(6, toEliminate.Count());
        }
        [TestMethod]
        public void ForSquares8x9To9x9Returns6SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(8, 9), new Square(9, 9) };
            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
            Assert.AreEqual(6, toEliminate.Count());
        }
    }
}