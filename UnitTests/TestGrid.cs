using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model.UnitTests
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void GetAvailablePlacementsForShipReturns2PlacementsForShipOfLength3InHorizontalGrid1X4()
        {
            Grid g = new Grid(1, 4);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(3, result.First().Count());
            Assert.AreEqual(3, result.Last().Count());
        }
        [TestMethod]
        public void GetAvailablePlacementsForShipReturns3PlacementsForShipOfLength3InVerticalGrid5X1()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(3, result.Count());


            foreach(var sequence in result)
            Assert.AreEqual(3, sequence.Count());
        }


        [TestMethod]
        public void GetAvailablePlacementsForShipReturns3PlacementsForShipOfLength2InHorizontalGrid1X6AfterSquareIsEleminated()
        {
            Grid g = new Grid(1, 6);
            g.EleminateSquares(new List<Square> { new Square(0, 2) });
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetAvailablePlacementsForShipReturns2PlacementsForShipOfLength2InVerticalGrid5X1AfterSquareIsEliminated()
        {
            Grid g = new Grid(5, 1);
            g.EleminateSquares(new List<Square> { new Square(1, 0) });
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(2, result.Count());

            foreach (var sequence in result)
                Assert.AreEqual(2, sequence.Count());

        }

        [TestMethod]
        public void GetSquaresNextToReturns3SquaresForGrid1x5ToRightSquare0x1()
        {
            Grid g = new Grid(1,5 );
            var result = g.GetSquaresNextTo(new Square(0,1), Direction.Right);
            Assert.AreEqual(3, result.Count());

        }
        [TestMethod]
        public void GetSquaresNextToReturns1SquaresForGrid1x5LeftToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Left);
            Assert.AreEqual(1, result.Count());

        }
        [TestMethod]
        public void GetSquaresNextToReturns0SquaresForGrid1x5AboveAndBeloweToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Up);
            Assert.AreEqual(0, result.Count());

            result = g.GetSquaresNextTo(new Square(0, 1), Direction.Down);
            Assert.AreEqual(0, result.Count());

        }

        [TestMethod]
        public void GetSquaresNextToReturns3SquaresForGrid5x1ToBelowSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Down);
            Assert.AreEqual(3, result.Count());

        }
        [TestMethod]
        public void GetSquaresNextToReturns1SquaresForGrid5x1AboveToSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Up);
            Assert.AreEqual(1, result.Count());

        }
        [TestMethod]
        public void GetSquaresNextToReturns0SquaresForGrid5x1RightAndLeftToSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Right);
            Assert.AreEqual(0, result.Count());

            result = g.GetSquaresNextTo(new Square(1, 0), Direction.Left);
            Assert.AreEqual(0, result.Count());

        }

    }
}

