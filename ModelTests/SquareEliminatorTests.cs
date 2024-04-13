using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.OOM.Battleship.Model.Tests
{
    [TestClass]
    public class SquareEliminatorTests
    {
        [TestMethod]
        public void FoeSquares4x3Returns18SquaresIncludingSurrondingSquares()
        {
            var eliminator =new SquareEliminator();
            var shipSquares= new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4,6) };
            Assert.AreEqual(18, eliminator.ToEliminate(shipSquares.Count()));
        }
    }
}