using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquareWithRowAndColumnProvided()
        {
            var square = new Square(2, 4);

            Assert.IsNotNull(square);
            Assert.AreEqual(2, square.Row);
            Assert.AreEqual(4, square.Column);
        }
    }
}