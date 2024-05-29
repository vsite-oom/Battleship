namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class ShotsGridTests
    {
        [TestMethod]
        public void GetSquaresInDirectionReturns3SquresAboveSquare3x3()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 3;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
            Assert.AreEqual(3, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns4SquresRightFromSquare3x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 5;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
            Assert.AreEqual(4, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns2SquresBelowSquare7x5()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 5;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
            Assert.AreEqual(2, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns1SqureLeftFromSquare7x1()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 1;
            var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
            Assert.AreEqual(1, squares.Count());
        }





        [TestMethod]
        public void GetSquaresInDirectionReturns1SquresAboveSquare1x3IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 3;
            grid.ChangeSquareState(1,3, SquareState.Hit);
            var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
            Assert.AreEqual(1, squares.Count());
        }

        [TestMethod]
        public void GetSquaresInDirectionReturns1SquresAboveSquare2x3IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 3;
            grid.ChangeSquareState(2, 3, SquareState.Hit);
            var squares = grid.GetSquaresInDirection(row, column, Direction.Upwards);
            Assert.AreEqual(0, squares.Count());
        }




        [TestMethod]
        public void GetSquaresInDirectionReturns2SquresRightFromSquare3x5IfSquare3x8IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 3;
            int column = 5;
            grid.ChangeSquareState(3, 8, SquareState.Hit);
            var squares = grid.GetSquaresInDirection(row, column, Direction.Rightwards);
            Assert.AreEqual(2, squares.Count());
        }



        [TestMethod]
        public void GetSquaresInDirectionReturns1SquresBelowSquare7x5ISquare9x5IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 5;
            grid.ChangeSquareState(9, 5, SquareState.Hit);
            var squares = grid.GetSquaresInDirection(row, column, Direction.Downwards);
            Assert.AreEqual(1, squares.Count());
        }
        [TestMethod]
        public void GetSquaresInDirectionReturns0SquresLeftFromSquare7x0IsHit()
        {
            var grid = new ShotsGrid(10, 10);
            int row = 7;
            int column = 1;
            grid.ChangeSquareState(7, 0, SquareState.Hit);
            var squares = grid.GetSquaresInDirection(row, column, Direction.Leftwards);
            Assert.AreEqual(0, squares.Count());
        }
    }
}