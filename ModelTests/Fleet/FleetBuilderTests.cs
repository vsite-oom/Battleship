using Model;

namespace ModelTests
{
    [TestClass]
    public class FleetBuilderTests
    {
        [TestMethod]
        public void CreateFleet_CreatesCorrectNumberOfShips()
        {
            var builder = new FleetBuilder(10, 10, new[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            var fleet = builder.CreateFleet();

            Assert.AreEqual(10, fleet.Ships.Count());
        }

        [TestMethod]
        public void CreateFleet_ShipsHaveCorrectLengths()
        {
            var builder = new FleetBuilder(10, 10, new[] { 3, 2 });
            var fleet = builder.CreateFleet();

            Assert.AreEqual(2, fleet.Ships.Count());
        }
    }
}
