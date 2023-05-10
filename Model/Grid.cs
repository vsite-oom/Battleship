using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;



namespace Vsite.Oom.Battleship.Model

{
    

    using SquareSequance = IEnumerable<Square>;
    using Sequances = IEnumerable<IEnumerable<Square>>;
    using squareAccess = Func<int, int, Square>;

    public enum Direction
    {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }
    public class Grid
    {

        
        readonly public int rows;
        readonly public int columns;
        private Square[,] squares;
        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            squares = new Square[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    squares[r, c] = new Square(r, c);

                }
            }
        }
        public SquareSequance AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public Sequances GetAvaliableSequences(int length)
        {
            if (length == 1) return GetAvaliableHorizontalSequances(length);
            return GetAvaliableHorizontalSequances(length).Concat(GetAvaliableVerticalSequances(length));



        }
        private bool IsSquareAvaliable(Square square) {
            return square != null && square.squareState == SquareState.Initial;
        }
        private Sequances GetAvalableSequences(int outerloopLimitint, int innerLoopLimit, squareAccess sq, int length)
        {
            var result = new List<SquareSequance>();
            for (int o = 0; o < outerloopLimitint; o++)
            {

                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; i++)
                {
                    if (IsSquareAvaliable(sq(o,i)))
                    {

                        queue.Enqueue(sq(o, i));
                        if (queue.Count == length)
                        {
                            result.Add(queue.ToArray());

                        }
                    }
                    else
                    {

                        queue.Clear();
                        if (innerLoopLimit - i <= length) break;

                    }
                }
            }
            return result;
        }
        private Sequances GetAvaliableHorizontalSequances(int length)
        {
            return GetAvalableSequences(rows, columns, (a, b) => squares[a, b], length);

        }
        private Sequances GetAvaliableVerticalSequances(int length)
        {
            return GetAvalableSequences(columns, rows, (a, b) => squares[b, a], length);

        }
        public void RemoveSquare(int row, int column) { squares[row, column] = null; }
        public void RemoveSquares(IEnumerable<Square> squares)
        {
            foreach(Square square in squares)
            {
                RemoveSquare(square.row, square.column);
            }
        }

        public void MarkSquare(int row, int column, HitResult result)
        {
            squares[row,column].Mark(result);
        }

        public SquareSequance GetAvailableSequence(Square from, Direction direction)
        {
            int row = from.row;
            int endRow = row;
            int deltaRow = 0;
            if (direction == Direction.Upwards)
            {
                --row;
                deltaRow = -1;
                endRow = -1;
            }
            else if (direction == Direction.Downwards)
            {
                ++row;
                deltaRow = +1;
                endRow = rows;
            }
            int column = from.column;
            int endColumn = column;
            int deltaColumn = 0;
            if (direction == Direction.Leftwards)
            {
                --column;
                deltaColumn = -1;
                endColumn = -1;
            }
            else if (direction == Direction.Rightwards)
            {
                ++column;
                deltaColumn = +1;
                endColumn = columns;
            }
            var result = new List<Square>();
            while (row != endRow || column != endColumn)
            {
                if (squares[row, column].squareState != SquareState.Initial)
                {
                    break;
                }
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }
    }
}
