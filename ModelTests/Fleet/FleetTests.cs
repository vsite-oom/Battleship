using Model;

namespace ModelTests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void CreateShip_AddsShipToFleet()
        {
            var fleet = new Fleet();
            fleet.CreateShip(new[] { new Square(0, 0), new Square(0, 1) });
            Assert.AreEqual(1, fleet.Ships.Count());
        }

        [TestMethod]
        public void Hit_ReturnsMissed_WhenNoShipAtPosition()
        {
            var fleet = new Fleet();
            fleet.CreateShip(new[] { new Square(0, 0), new Square(0, 1) });
            Assert.AreEqual(HitResult.Missed, fleet.Hit(5, 5));
        }

        [TestMethod]
        public void Hit_ReturnsHit_WhenShipIsHit()
        {
            var fleet = new Fleet();
            fleet.CreateShip(new[] { new Square(0, 0), new Square(0, 1) });
            Assert.AreEqual(HitResult.Hit, fleet.Hit(0, 0));
        }

        [TestMethod]
        public void Hit_ReturnsSunken_WhenShipIsDestroyed()
        {
            var fleet = new Fleet();
            fleet.CreateShip(new[] { new Square(2, 0), new Square(2, 1) });
            fleet.Hit(2, 0);
            Assert.AreEqual(HitResult.Sunken, fleet.Hit(2, 1));
        }
    }
}
