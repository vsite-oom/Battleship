using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSurroundingSquareEliminator
    {
        [TestMethod]
        public void ToEliminateReturns18SquaresForShip4_3To4_6()
        {
            var squares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);
            var result = eliminator.ToEliminate(squares);

            Assert.AreEqual(18, result.Count());

            Assert.IsTrue(result.Contains(new Square(4, 3)));
            Assert.IsTrue(result.Contains(new Square(4, 4)));
            Assert.IsTrue(result.Contains(new Square(4, 5)));
            Assert.IsTrue(result.Contains(new Square(4, 6)));

            Assert.IsTrue(result.Contains(new Square(3, 2)));
            Assert.IsTrue(result.Contains(new Square(3, 5)));
            Assert.IsTrue(result.Contains(new Square(5, 2)));
            Assert.IsTrue(result.Contains(new Square(5, 6)));
        }

        [TestMethod]
        public void ToEliminateReturns8SquaresForShip0_3To0_4()
        {
            var squares = new List<Square> { new Square(0, 3), new Square(0, 4) };
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);
            var result = eliminator.ToEliminate(squares);

            Assert.AreEqual(8, result.Count());

            Assert.IsTrue(result.Contains(new Square(0, 3)));
            Assert.IsTrue(result.Contains(new Square(0, 4)));

            Assert.IsTrue(result.Contains(new Square(0, 2)));
            Assert.IsTrue(result.Contains(new Square(1, 2)));
            Assert.IsTrue(result.Contains(new Square(1, 3)));
            Assert.IsTrue(result.Contains(new Square(1, 4)));
            Assert.IsTrue(result.Contains(new Square(1, 5)));
            Assert.IsTrue(result.Contains(new Square(0, 5)));
        }

        [TestMethod]
        public void ToEliminateReturns8SquaresForShip3_9To4_9()
        {
            var squares = new List<Square> { new Square(3, 9), new Square(4, 9) };
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);
            var result = eliminator.ToEliminate(squares);

            Assert.AreEqual(8, result.Count());

            Assert.IsTrue(result.Contains(new Square(3, 9)));
            Assert.IsTrue(result.Contains(new Square(4, 9)));

            Assert.IsTrue(result.Contains(new Square(2, 9)));
            Assert.IsTrue(result.Contains(new Square(2, 8)));
            Assert.IsTrue(result.Contains(new Square(3, 8)));
            Assert.IsTrue(result.Contains(new Square(4, 8)));
            Assert.IsTrue(result.Contains(new Square(5, 8)));
            Assert.IsTrue(result.Contains(new Square(5, 9)));
        }

        [TestMethod]
        public void ToEliminateReturns12SquaresForShip7_5To9_5()
        {
            var squares = new List<Square> { new Square(7, 5), new Square(8, 5), new Square(9, 5) };
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);
            var result = eliminator.ToEliminate(squares);
            Assert.AreEqual(12, result.Count());

            Assert.IsTrue(result.Contains(new Square(7, 5)));
            Assert.IsTrue(result.Contains(new Square(8, 5)));
            Assert.IsTrue(result.Contains(new Square(9, 5)));

            Assert.IsTrue(result.Contains(new Square(6, 4)));
            Assert.IsTrue(result.Contains(new Square(9, 4)));
            Assert.IsTrue(result.Contains(new Square(6, 6)));
            Assert.IsTrue(result.Contains(new Square(9, 6)));
        }

        [TestMethod]
        public void ToEliminateReturns9SquaresForShip5_0To5_1()
        {
            var squares = new List<Square> { new Square(5, 0), new Square(5, 1) };
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);
            var result = eliminator.ToEliminate(squares);
            Assert.AreEqual(9, result.Count());

            Assert.IsTrue(result.Contains(new Square(5, 0)));
            Assert.IsTrue(result.Contains(new Square(5, 1)));

            Assert.IsTrue(result.Contains(new Square(4, 0)));
            Assert.IsTrue(result.Contains(new Square(4, 2)));
            Assert.IsTrue(result.Contains(new Square(6, 0)));
            Assert.IsTrue(result.Contains(new Square(6, 2)));
        }
    }
}
