using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareEliminatorTests
    {
        [TestMethod]
        public void ForSquares4x3To4x6Returns18SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(4, 3), new Square(4, 4), new Square(4, 5), new Square(4, 6) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);  // Na osnovu polja koja smo proslijedili za brod vraća koordinate svih polja koja treba eliminirati.
           
            Assert.AreEqual(18, toEliminate.Count());  

            var corners = new List<SquareCoordinate> { new SquareCoordinate(3, 2),
                                                       new SquareCoordinate(3, 7),
                                                       new SquareCoordinate(5, 2),
                                                       new SquareCoordinate(5, 7) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }
            
            Assert.AreEqual(0, corners.Count());  // Check if boundary coordinates are included (we check only corners, not all.)
        }

        [TestMethod]
        public void ForSquares3x9To4x9Returns8SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(3, 9), new Square(4, 9) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);
           
            Assert.AreEqual(8, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(2, 8), 
                                                       new SquareCoordinate(2, 9),
                                                       new SquareCoordinate(5, 8),
                                                       new SquareCoordinate(5, 9) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }
            
            Assert.AreEqual(0, corners.Count());
        }

        [TestMethod]
        public void ForSquares0x3To0x4Returns8SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(0, 3), new Square(0, 4) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

            Assert.AreEqual(8, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(0, 2),
                                                       new SquareCoordinate(0, 5),
                                                       new SquareCoordinate(1, 2),
                                                       new SquareCoordinate(1, 5) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }

            Assert.AreEqual(0, corners.Count());
        }

        [TestMethod]
        public void ForSquares5x0To5x1Returns9SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(5, 0), new Square(5, 1) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

            Assert.AreEqual(9, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(4, 0),
                                                       new SquareCoordinate(4, 2),
                                                       new SquareCoordinate(6, 0),
                                                       new SquareCoordinate(6, 2) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }

            Assert.AreEqual(0, corners.Count());
        }

        [TestMethod]
        public void ForSquares7x5To9x5Returns12SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(7, 5), new Square(8, 5), new Square(9, 5) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

            Assert.AreEqual(12, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(6, 4),
                                                       new SquareCoordinate(6, 6),
                                                       new SquareCoordinate(9, 4),
                                                       new SquareCoordinate(9, 6) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }

            Assert.AreEqual(0, corners.Count());
        }

        [TestMethod]
        public void ForSquares0x0To0x1Returns6SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(0, 0), new Square(0, 1) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

            Assert.AreEqual(6, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(0, 2),
                                                       new SquareCoordinate(1, 0),
                                                       new SquareCoordinate(1, 2) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }

            Assert.AreEqual(0, corners.Count());
        }

        [TestMethod]
        public void ForSquares8x9To9x9Returns6SquaresIncludingSurroundingSquares()
        {
            var eliminator = new SquareEliminator();

            var shipSquares = new List<Square> { new Square(8, 9), new Square(9, 9) };

            var toEliminate = eliminator.ToEliminate(shipSquares, 10, 10);

            Assert.AreEqual(6, toEliminate.Count());

            var corners = new List<SquareCoordinate> { new SquareCoordinate(7, 8),
                                                       new SquareCoordinate(7, 9),
                                                       new SquareCoordinate(9, 8) };
            foreach (var coordinate in toEliminate)
            {
                foreach (var corner in corners)
                {
                    if (coordinate.Row == corner.Row && coordinate.Column == corner.Column)
                    {
                        corners.Remove(corner);
                        break;
                    }
                }
            }

            Assert.AreEqual(0, corners.Count());
        }
    }
}