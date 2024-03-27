namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public readonly int rows;
        public readonly int columns;

        private readonly Square?[,] _squares;
        public IEnumerable<Square> Squares
        {
            get { return _squares.Cast<Square>().Where(x => x != null); }
        }


        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            _squares = new Square[rows, columns];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < columns; c++)
                    _squares[r, c] = new Square(r, c);

        }

    }
}
