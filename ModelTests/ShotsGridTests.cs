namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShotsGridTests
    {
        [TestMethod]
        public void GetSquaresInDirectionReturns3SquaresAboveSquare3x3()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 3;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
            Assert.AreEqual(3, squares.Count());
        }

        [TestMethod]
        public void GetSquaresInDirectionReturns4SquaresRightFromSquare3x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 5;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
            Assert.AreEqual(4, squares.Count());
        } 
        
        [TestMethod]
        public void GetSquaresInDirectionReturns2SquaresBelowSquare7x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 5;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
            Assert.AreEqual(2, squares.Count());
        }
        
        [TestMethod]
        public void GetSquaresInDirectionReturns1SquareLeftFromSquare7x1()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 1;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
            Assert.AreEqual(1, squares.Count());
        }

        //TODO: Implement methods for these tests to pass
    }
}