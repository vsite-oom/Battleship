using Vsite.Oom.Battleship.Model;

namespace ModelTests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquareAtGivenRowAndColumn()
        {
            int row = 3;
            int column = 5;
            var square = new Square(row, column);
            Assert.AreEqual(row, square.Row);
            Assert.AreEqual(column, square.Column);
        }
    }
}