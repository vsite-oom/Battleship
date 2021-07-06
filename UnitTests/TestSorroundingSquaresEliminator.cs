using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestSorroundingSquaresEliminator
    {

        [TestMethod]
        public void ToEliminateReturnsEighteenSquaresToEliminateForShipWithDimensionFourByThree()
        {
            var ship = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            var eliminator = new SurroundingSquareEliminator(10, 10);
            var toEliminate = eliminator.ToEliminate(ship);

            Assert.AreEqual(18, toEliminate.Count());

            Assert.IsTrue(toEliminate.Contains(new Square(3, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(3, 7)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 7)));
        }

        [TestMethod]
        public void ToEliminateReturnsEightSquaresWhenShipIsPlacedOnTheFirstColumnVertically()
        {
            var ship = new List<Square> { new Square(0, 5), new Square(0, 6)};
            var eliminator = new SurroundingSquareEliminator(10, 10);
            var toEliminate = eliminator.ToEliminate(ship);

            Assert.AreEqual(8, toEliminate.Count());

            // all sorrounding squares
            Assert.IsTrue(toEliminate.Contains(new Square(0, 4)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 4)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 7)));
            Assert.IsTrue(toEliminate.Contains(new Square(0, 7)));
        }

        [TestMethod]
        public void ToEliminateReturnsEightSquaresWhenShipIsPlacedOnTheFirsRowHorisontally()
        {
            var ship = new List<Square> { new Square(0, 0), new Square(0, 1) };
            var eliminator = new SurroundingSquareEliminator(10, 10);
            var toEliminate = eliminator.ToEliminate(ship);

            Assert.AreEqual(6, toEliminate.Count());
          
            // all sorrounding squares
            Assert.IsTrue(toEliminate.Contains(new Square(0, 0)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 0)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(0, 2)));
        }


        [TestMethod]
        public void ToEliminateReturnsSixSquaresWhenShipIsPlacedInTheCorner()
        {
            var ship = new List<Square>{ new Square(8, 9), new Square(9, 9)};
            var eliminator = new SurroundingSquareEliminator(10, 10);
            var toEliminate = eliminator.ToEliminate(ship);

            Assert.AreEqual(6, toEliminate.Count());

            // all sorrounding squares
            Assert.IsTrue(toEliminate.Contains(new Square(7, 8)));
            Assert.IsTrue(toEliminate.Contains(new Square(7, 9)));
            Assert.IsTrue(toEliminate.Contains(new Square(9, 8)));
            Assert.IsTrue(toEliminate.Contains(new Square(9, 9)));
        }

        // TODO: test cases when ship is placed on the mirrored way as now ( to cover all sides of the rect ) 
        // TODO: test cases for all ship dimensions, covered are 2 and 4, so 3 and 5 are missing
    }
}
