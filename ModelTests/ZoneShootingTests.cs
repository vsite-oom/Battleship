using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class ZoneShootingTests
    {
        [TestMethod]
        public void NextTargetReturnsOneOfSurroundingSquaresForFirstSquare4_4()
        {
            var ZoneShooting = new ZoneShooting(new Grid(10, 10), new Square(4, 4), new int[] { 5 });
            var result =ZoneShooting.NextTarget();
            Assert.IsNotNull(result);
            Square[] possibleResults = { new Square(3, 4), new Square(4, 3), new Square(4, 5), new Square(5, 4), };
            Assert.IsTrue(possibleResults.Contains(result));





        }
    }
}