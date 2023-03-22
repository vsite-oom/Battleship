namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for(int r = 0; r < Rows; ++r)
            {
                for(int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public IEnumerable<Square> AvailableSquares()
        {
            return squares.Cast<Square>();
            //throw new NotImplementedException();
        }
    }
}
