using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareEliminatorTests
    {
        [TestMethod]
        public void ForSquares4x3to4x6Returns18SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };


            Assert.AreEqual(18, eliminator.ToEliminate(shipSquares, 10, 10).Count());
        }


        [TestMethod]
        public void ForSquares3x9to4x9Returns18SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();
            var shipSquares = new List<Square> { new Square(3, 9), new Square(4, 9) };


            Assert.AreEqual(18, eliminator.ToEliminate(shipSquares, 10, 10).Count());
        }
    }
}