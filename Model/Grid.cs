using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }
    public class Grid
    {

        public Grid(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;

            squares = new Square?[rows, columns];
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }
        public Grid(int rows, int columns, ISquareEliminator squareEliminator) : this(rows, columns)
        {
            this.squareEliminator = squareEliminator;
        }

        // niz nizova polja
        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {
            List<List<Square>> result = GetHorizontalPlacements(length);
            if (length > 1)
                result.AddRange(GetVerticalPlacements(length));
            return result;
        }

        public IEnumerable<Square> GetAvailablePlacementsInDirection(Square from, Direction direction)
        {
            int deltaRow = 0;
            int deltaColumn = 0;
            int count = 0;
            switch (direction)
            {
                case Direction.Upwards:
                    deltaRow = -1;
                    count = from.Row + 1;
                    break;
                case Direction.Rightwards:
                    deltaColumn = +1;
                    count = Columns - from.Column;
                    break;
                case Direction.Downwards:
                    deltaRow = +1;
                    count = Rows - from.Row;
                    break;
                case Direction.Leftwards:
                    deltaColumn = -1;
                    count = from.Column + 1;
                    break;
            }
            List<Square> result = new List<Square>();
            int row = from.Row + deltaRow;
            int column = from.Column + deltaColumn;
            for (int i = 1; i < count; ++i)
            {
                if (squares[row, column].Value.SquareState != SquareState.Default)
                    break;
                result.Add(squares[row, column].Value);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }

        public void RecordResult(Square square, HitResult result)
        {
            square.SetSquareState(result);
            squares[square.Row, square.Column] = square;
        }

        private List<List<Square>> GetHorizontalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> gathered = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null && squares[r, c].Value.SquareState == SquareState.Default)
                        gathered.Enqueue(squares[r, c].Value);
                    else
                        gathered.Clear();
                    if (gathered.Count == length)
                    {
                        result.Add(new List<Square>(gathered.ToArray<Square>()));
                    }
                }
            }
            return result;
        }

        private List<List<Square>> GetVerticalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                LimitedQueue<Square> gathered = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; ++r)
                {
                    if (squares[r, c] != null && squares[r, c].Value.SquareState == SquareState.Default)
                        gathered.Enqueue(squares[r, c].Value);
                    else
                        gathered.Clear();
                    if (gathered.Count == length)
                    {
                        result.Add(new List<Square>(gathered.ToArray<Square>()));
                    }
                }
            }
            return result;
        }

        public void Eliminate(IEnumerable<Square> selected)
        {
            var toEliminate = squareEliminator.ToEliminate(selected);
            foreach (Square square in toEliminate)
            {
                squares[square.Row, square.Column] = null;
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        // dvodiemnzionalni nullable niz
        private Square?[,] squares;
        private ISquareEliminator squareEliminator = new OnlyShipSquaresEliminator();
    }
}