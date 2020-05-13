using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void GetAvailablePlacementsForShipRetruns2PlacmentsForShipOfLenght3InHorizontalGrid1x4()
        {
            Grid g = new Grid(1, 4);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(3, result.First().Count());
            Assert.AreEqual(3, result.Last().Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForShipRetruns3PlacmentsForShipOfLenght3InVerticalGrid5x1()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(3, result.Count());

            foreach (var sequence in result)
            {
                Assert.AreEqual(3, sequence.Count());
            }
   
        }


        [TestMethod]
        public void GetAvailablePlacementsForShipRetruns3PlacmentsForShipOfLenght2InHorizontalGrid1x6AfterSquareIsEliminated()
        {
            Grid g = new Grid(1, 6);
            g.EliminateSquares(new List<Square> { new Square(0, 2) });
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForShipRetruns2PlacmentsForShipOfLenght2InVerticalGrid5x1AfterSquareIsEliminated()
        {
            Grid g = new Grid(5, 1);
            g.EliminateSquares(new List<Square> { new Square(1, 0) });
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(2, result.Count());

            foreach (var sequence in result)
            {
                Assert.AreEqual(2, sequence.Count());
            }

        }

        [TestMethod]
        public void GetSqaresNextToReturns3SquaresForGrid1x5RightToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1),Direction.Right);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetSqaresNextToReturns1SquaresForGrid1x5LeftToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Left);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetSqaresNextToReturns0SquaresForGrid1x5AboveAndBelowSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Up);
            Assert.AreEqual(0, result.Count());

            result = g.GetSquaresNextTo(new Square(0, 1), Direction.Down);
            Assert.AreEqual(0, result.Count());
        }


        [TestMethod]
        public void GetSqaresNextToReturns3SquaresForGrid5x1BelowToSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Down);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetSqaresNextToReturns1SquaresForGrid5x1LeftToSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Up);
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetSqaresNextToReturns0SquaresForGrid5x1AboveAndBelowSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Right);
            Assert.AreEqual(0, result.Count());

            result = g.GetSquaresNextTo(new Square(1, 0), Direction.Left);
            Assert.AreEqual(0, result.Count());
        }




    }
}
