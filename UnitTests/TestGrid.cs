using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vsite.Oom.Battleship.Model
{
    [TestClass]
    public class TestGrid
    {
        [TestMethod]
        public void getAvaliablePlacementsFroShipReturns2PlacementsForShipOfLenght3InHorizontalGrid1x4()
        {
            Grid g = new Grid(1, 4);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(2, result.Count());

            Assert.AreEqual(3, result.First().Count());
            Assert.AreEqual(3, result.Last().Count());
        }
        [TestMethod]
        public void getAvaliablePlacementsFroShipReturns3PlacementsForShipOfLenght3InVerticalGrid5x1()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetAvailablePlacements(3);
            Assert.AreEqual(3, result.Count());

            foreach (var seq in result)
            {
                Assert.AreEqual(3, seq.Count());
            }
        }
        [TestMethod]
        public void getAvaliablePlacementsFroShipReturns3PlacementsForShipOfLenght2InHorizontalGrid1x6AfterSquareIsElimianted()
        {
            Grid g = new Grid(1, 6);
            g.eliminateSquares(new List<Square> { new Square(0,2)});
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(3, result.Count());

        }
        [TestMethod]
        public void getAvaliablePlacementsFroShipReturns2PlacementsForShipOfLenght2InVerticalGrid5x1AfterSquareIsElimianted()
        {
            Grid g = new Grid(5, 1);
            g.eliminateSquares(new List<Square> { new Square(1, 0) });
            var result = g.GetAvailablePlacements(2);
            Assert.AreEqual(2, result.Count());

            foreach (var seq in result)
            {
                Assert.AreEqual(2, seq.Count());
            }
        }
        [TestMethod]
        public void GetSquaresNextToReturns3SquaresForGrid1x5RightToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1),Direction.Right);
            Assert.AreEqual(3, result.Count());
        }
        [TestMethod]
        public void GetSquaresNextToReturns1SquareForGrid1x5LeftToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Left);
            Assert.AreEqual(1, result.Count());
        }
        [TestMethod]
        public void GetSquaresNextToReturns0SquaresForGrid1x5AboveAndBelowToSquare0x1()
        {
            Grid g = new Grid(1, 5);
            var result = g.GetSquaresNextTo(new Square(0, 1), Direction.Up);
            Assert.AreEqual(0, result.Count());

            result = g.GetSquaresNextTo(new Square(0, 1), Direction.Down);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetSquaresNextToReturns3SquaresForGrid5x1BelowToSquare1x0()
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
        public void GetSquaresNextToReturns0SquaresForGrid5x1LeftAndRightToSquare1x0()
        {
            Grid g = new Grid(5, 1);
            var result = g.GetSquaresNextTo(new Square(1, 0), Direction.Left);
            Assert.AreEqual(0, result.Count());



            result = g.GetSquaresNextTo(new Square(1, 0), Direction.Right);
            Assert.AreEqual(0, result.Count());
        }
    }
}
