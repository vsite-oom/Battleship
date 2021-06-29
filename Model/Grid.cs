using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            squares = new Square?[rows, columns];

            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                    squares[r, c] = new Square(r, c);
            }
        }

        public IEnumerable<IEnumerable<Square>> GetSequences(int length)
        {
            var result = GetHorizontalSequences(length);
            if (length > 1)
                result.AddRange(GetVerticalSequences(length));
            return result;
        }

        public void RemoveSquares(IEnumerable<Square> squaresToEliminate)
        {
            foreach (Square square in squaresToEliminate)
            {
                squares[square.Row, square.Column] = null;
            }
        }

        public IEnumerable<Square> GetSequence(Square reference, Direction direction)
        {
            int deltaRow = 0;
            int deltaColumn = 0;
            int count = 0;
            switch (direction)
            {
                case Direction.Up:
                    deltaRow = -1;
                    count = reference.Row;
                    break;
                case Direction.Right:
                    deltaColumn = +1;
                    count = Columns - reference.Column - 1;
                    break;
                case Direction.Down:
                    deltaRow = +1;
                    count = Rows - reference.Row - 1;
                    break;
                case Direction.Left:
                    deltaColumn = -1;
                    count = reference.Column;
                    break;
            }
            List<Square> result = new List<Square>();
            int row = reference.Row + deltaRow;
            int column = reference.Column + deltaColumn;
            for (int i = 0; i < count; ++i)
            {
                if (!IsSquareAvailable(row, column))
                    break;
                else
                {
                    result.Add(squares[row, column].Value);
                    row += deltaRow;
                    column += deltaColumn;
                }
            }
            return result;
        }

        public void MarkSquare(Square square, SquareState state)
        {
            square.SetState(state);
            squares[square.Row, square.Column] = square;
        }

        private bool IsSquareAvailable(int row, int column)
        {
            return squares[row, column] != null && squares[row, column].Value.SquareState == SquareState.Default;
        }

        private List<IEnumerable<Square>> GetHorizontalSequences(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            for (int r = 0; r < Rows; ++r)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; ++c)
                {
                    if (IsSquareAvailable(r, c))
                    {
                        queue.Enqueue(squares[r, c].Value);
                        if (queue.Count >= length)
                            result.Add(queue.ToArray());
                    }
                    else
                        queue.Clear();
                }
            }
            return result;
        }

        private List<IEnumerable<Square>> GetVerticalSequences(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; ++r)
                {
                    if (IsSquareAvailable(r, c))
                    {
                        queue.Enqueue(squares[r, c].Value);
                        if (queue.Count >= length)
                            result.Add(queue.ToArray());
                    }
                    else
                        queue.Clear();
                }
            }
            return result;
        }

        private readonly Square?[,] squares;

        public readonly int Rows;
        public readonly int Columns;
    }
}
