using Model;

namespace ModelTests
{
    [TestClass]
    public class ShipTests
    {
        [TestMethod]
        public void Constructor_SetsLength()
        {
            var ship = new Ship(new[] { new Square(0, 0), new Square(0, 1), new Square(0, 2) });
            Assert.AreEqual(3, ship.Length);
        }

        [TestMethod]
        public void Hit_ReturnsHit_WhenSquareBelongsToShip()
        {
            var ship = new Ship(new[] { new Square(1, 0), new Square(1, 1) });
            Assert.AreEqual(HitResult.Hit, ship.Hit(1, 0));
        }

        [TestMethod]
        public void Hit_ReturnsMissed_WhenSquareNotOnShip()
        {
            var ship = new Ship(new[] { new Square(1, 0), new Square(1, 1) });
            Assert.AreEqual(HitResult.Missed, ship.Hit(3, 3));
        }

        [TestMethod]
        public void Hit_ReturnsSunken_WhenAllSquaresHit()
        {
            var ship = new Ship(new[] { new Square(0, 0), new Square(0, 1) });
            ship.Hit(0, 0);
            Assert.AreEqual(HitResult.Sunken, ship.Hit(0, 1));
        }
    }
}
