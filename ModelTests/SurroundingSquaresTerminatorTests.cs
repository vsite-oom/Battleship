using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class SurroundingSquaresTerminatorTests
    {
        [TestMethod]
        public void ToEliminateReturns18SquaresForShip4_3To4_6ForGrid10x10()
        {
            var grid = new FleetGrid(10, 10);
            Square[] squares = { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };
            var terminator = new SurroundingSquaresTerminator(10, 10);
            var toEliminate = terminator.ToEliminate(squares);

            Assert.AreEqual(18, toEliminate.Count());
            Assert.IsTrue(toEliminate.Contains(squares[0]));
            Assert.IsTrue(toEliminate.Contains(squares[1]));
            Assert.IsTrue(toEliminate.Contains(squares[2]));
            Assert.IsTrue(toEliminate.Contains(squares[3]));

            Assert.IsTrue(toEliminate.Contains(new Square(3, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(3, 7)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 7)));
        }
        
        
        [TestMethod]
        public void ToEliminateReturns8SquaresForShip0_3To0_4ForGrid10x10()
        {
            var grid = new FleetGrid(10, 10);
            Square[] squares = { new Square(0, 3), new Square(0, 4) };
            var terminator = new SurroundingSquaresTerminator(10, 10);
            var toEliminate = terminator.ToEliminate(squares);

            Assert.AreEqual(8, toEliminate.Count());
            Assert.IsTrue(toEliminate.Contains(squares[0]));
            Assert.IsTrue(toEliminate.Contains(squares[1]));

            Assert.IsTrue(toEliminate.Contains(new Square(0, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(0, 5)));
            Assert.IsTrue(toEliminate.Contains(new Square(1, 5)));
        }
        
        
        [TestMethod]
        public void ToEliminateReturns8SquaresForShip3_9To4_9ForGrid10x10()
        {
            var grid = new FleetGrid(10, 10);
            Square[] squares = { new Square(3, 9), new Square(4, 9) };
            var terminator = new SurroundingSquaresTerminator(10, 10);
            var toEliminate = terminator.ToEliminate(squares);

            Assert.AreEqual(8, toEliminate.Count());
            Assert.IsTrue(toEliminate.Contains(squares[0]));
            Assert.IsTrue(toEliminate.Contains(squares[1]));

            Assert.IsTrue(toEliminate.Contains(new Square(2, 8)));
            Assert.IsTrue(toEliminate.Contains(new Square(2, 9)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 8)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 9)));
        } 
        


        [TestMethod]
        public void ToEliminateReturns12SquaresForShip7_5To9_5ForGrid10x10()
        {
            var grid = new FleetGrid(10, 10);
            Square[] squares = { new Square(7, 5), new Square(8, 5), new Square(9, 5) };
            var terminator = new SurroundingSquaresTerminator(10, 10);
            var toEliminate = terminator.ToEliminate(squares);

            Assert.AreEqual(12, toEliminate.Count());
            Assert.IsTrue(toEliminate.Contains(squares[0]));
            Assert.IsTrue(toEliminate.Contains(squares[1]));
            Assert.IsTrue(toEliminate.Contains(squares[2]));

            Assert.IsTrue(toEliminate.Contains(new Square(6, 4)));
            Assert.IsTrue(toEliminate.Contains(new Square(6, 5)));
            Assert.IsTrue(toEliminate.Contains(new Square(6, 6)));
            Assert.IsTrue(toEliminate.Contains(new Square(9, 4)));
            Assert.IsTrue(toEliminate.Contains(new Square(9, 6)));
        }


        [TestMethod]
        public void ToEliminateReturns9SquaresForShip5_0To5_1ForGrid10x10()
        {
            var grid = new FleetGrid(10, 10);
            Square[] squares = { new Square(5, 0), new Square(5, 1) };
            var terminator = new SurroundingSquaresTerminator(10, 10);
            var toEliminate = terminator.ToEliminate(squares);

            Assert.AreEqual(9, toEliminate.Count());
            Assert.IsTrue(toEliminate.Contains(squares[0]));
            Assert.IsTrue(toEliminate.Contains(squares[1]));

            Assert.IsTrue(toEliminate.Contains(new Square(4, 0)));
            Assert.IsTrue(toEliminate.Contains(new Square(4, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(6, 0)));
            Assert.IsTrue(toEliminate.Contains(new Square(6, 2)));
            Assert.IsTrue(toEliminate.Contains(new Square(5, 2)));
        }
    }
}