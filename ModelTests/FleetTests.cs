namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void ConstructorCreatesEmptyFleet()
        {
            var fleet = new Fleet();

            Assert.AreEqual(0, fleet.Ships.Count());
        }
    }
}
