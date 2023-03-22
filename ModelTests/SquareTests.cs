using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquareWithRowAndColumnProvided()
        {
            var square = new Square(3, 4);
            Assert.AreEqual(3, square.Row);
            Assert.AreEqual(4, square.Column);
        }
    }
}