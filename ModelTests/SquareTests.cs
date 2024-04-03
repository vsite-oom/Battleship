namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquareWithRowAndColumnProvided()
        {
            int row = 5;
            int column=6;
            var square = new Square(row, column);
            Assert.AreEqual(row, square.Row);
            Assert.AreEqual(column, square.Column);
        }
    }
}