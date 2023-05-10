using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class LineShootingTests
    {
        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare5_6And5_7()
        {
            var ZoneShooting = new LineShooting(new Grid(10, 10), new Square[]{ new Square(5, 6),new Square(5,7) }, new int[] { 5 });
            var result = ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(5, 5), new Square(5, 8), };
            Assert.IsTrue(possibleResults.Contains(result));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare5_7And5_6()
        {
            var ZoneShooting = new LineShooting(new Grid(10, 10), new Square[] {new Square(5, 7), new Square(5, 6) }, new int[] { 5 });
            var result = ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(5, 5), new Square(5, 8), };
            Assert.IsTrue(possibleResults.Contains(result));
        }
        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare5_6And6_6()
        {
            var ZoneShooting = new LineShooting(new Grid(10, 10), new Square[] { new Square(5, 6), new Square(6, 6) }, new int[] { 5 });
            var result = ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(4,6), new Square(7, 6), };
            Assert.IsTrue(possibleResults.Contains(result));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare6_6And5_6And7_6()
        {
            var ZoneShooting = new LineShooting(new Grid(10, 10), new Square[] { new Square(6, 6), new Square(5, 6), new Square(7, 6) }, new int[] { 5 });
            var result = ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(4, 6), new Square(8, 6), };
            Assert.IsTrue(possibleResults.Contains(result));
        }

        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare0_6And2_6And1_6()
        {
            var ZoneShooting = new LineShooting(new Grid(10, 10), new Square[] { new Square(0, 6), new Square(2, 6), new Square(1, 6) }, new int[] { 5 });
            var result = ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(3, 6) };
            Assert.IsTrue(possibleResults.Contains(result));
        }
    }
}