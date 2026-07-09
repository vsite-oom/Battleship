using Model;

namespace ModelTests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void Constructor_SetsRowAndColumn()
        {
            var square = new Square(3, 5);
            Assert.AreEqual(3, square.Position.Row);
            Assert.AreEqual(5, square.Position.Column);
        }
    }
}
