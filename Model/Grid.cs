using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace Vsite.Oom.Battleship.Model
{
    using Sequences = IEnumerable<IEnumerable<Square>>;
    using SqaureAccess = Func<int, int, Square>;
    using SquareSequence = IEnumerable<Square>;

    public enum Direction
    {
        Upwards,
        Rightwards,
        Downwards,
        Leftwards
    }

    //public class Grid
    //{
    //    public Grid(int rows, int columns)
    //    {
    //        Rows = rows;
    //        Columns = columns;
    //        squares = new Square[Rows, Columns];
    //        for (int r = 0; r < Rows; ++r)
    //        {
    //            for (int c = 0; c < Columns; ++c)
    //            {
    //                squares[r, c] = new Square(r, c);
    //            }
    //        }
    //    }

    //    public readonly int Rows;
    //    public readonly int Columns;

    //    private readonly Square[,] squares;

    //    public SquareSequence AvailableSquares()
    //    {
    //        return squares.Cast<Square>().Where(sq => sq != null);
    //    }

    //    public void RemoveSquare(int row, int column)
    //    {
    //        squares[row, column] = null;
    //    }

    //    public void RemoveSquares(SquareSequence squaresToRemove)
    //    {
    //        foreach (var square in squaresToRemove)
    //        {
    //            RemoveSquare(square.Row, square.Column);
    //        }
    //    }

    //    public Sequences GetAvailableSequences(int length)
    //    {
    //        var result = GetAvailableHorizontalSequences(length);
    //        if (length == 1)
    //        {
    //            return result;
    //        }
    //        return result.Concat(GetAvailableVerticalSequences(length));
    //    }

    //    private Sequences GetAvailableHorizontalSequences(int length)
    //    {
    //        return GetAvailableSequences(Rows, Columns, (a, b) => squares[a, b], length);
    //    }
    //    private Sequences GetAvailableVerticalSequences(int length)
    //    {
    //        return GetAvailableSequences(Columns, Rows, (a, b) => squares[b, a], length);
    //    }

    //    private bool IsAvailable(Square square)
    //    {
    //        return square != null && square.SquareState == SquareState.Initial;
    //    }

    //    private Sequences GetAvailableSequences(int outerLoopLimit, int innerLoopLimit, SqaureAccess squareAccess, int length)
    //    {
    //        var result = new List<SquareSequence>();
    //        for (int o = 0; o < outerLoopLimit; ++o)
    //        {
    //            var queue = new LimitedQueue<Square>(length);
    //            for (int i = 0; i < innerLoopLimit; ++i)
    //            {
    //                if (IsAvailable(squareAccess(o, i)))
    //                {
    //                    queue.Enqueue(squareAccess(o, i));
    //                    if (queue.Count == length)
    //                    {
    //                        result.Add(queue.ToArray());
    //                    }
    //                }
    //                else
    //                {
    //                    queue.Clear();
    //                }
    //            }
    //        }
    //        return result;
    //    }

    //    public void MarkSquare(int row, int column, HitResult hitResult)
    //    {
    //        squares[row, column].Mark(hitResult);
    //    }

    //    public SquareSequence GetAvailableSequence(Square from, Direction direction)
    //    {
    //        int row = from.Row;
    //        int endRow = row;
    //        int deltaRow = 0;
    //        if (direction == Direction.Upwards)
    //        {
    //            --row;
    //            deltaRow = -1;
    //            endRow = -1;
    //        }
    //        else if (direction == Direction.Downwards)
    //        {
    //            ++row;
    //            deltaRow = +1;
    //            endRow = Rows;
    //        }
    //        int column = from.Column;
    //        int endColumn = column;
    //        int deltaColumn = 0;
    //        if (direction == Direction.Leftwards)
    //        {
    //            --column;
    //            deltaColumn = -1;
    //            endColumn = -1;
    //        }
    //        else if (direction == Direction.Rightwards)
    //        {
    //            ++column;
    //            deltaColumn = +1;
    //            endColumn = Columns;
    //        }
    //        var result = new List<Square>();
    //        while (row != endRow || column != endColumn)
    //        {
    //            if (squares[row, column].SquareState != SquareState.Initial)
    //            {
    //                break;
    //            }
    //            result.Add(squares[row, column]);
    //            row += deltaRow;
    //            column += deltaColumn;
    //        }
    //        return result;
    //    }

    //    public void Eliminate(int row, int column)
    //    {
    //        squares[row, column].Eliminate();
    //    }
    //}
    public class FleetGrid
    {
        public FleetGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public void RemoveSquares(SquareSequence squaresToRemove)
        {
            foreach (var square in squaresToRemove)
            {
                RemoveSquare(square.Row, square.Column);
            }
        }

        public Sequences GetAvailableSequences(int length)
        {
            var result = GetAvailableHorizontalSequences(length);
            if (length == 1)
            {
                return result;
            }
            return result.Concat(GetAvailableVerticalSequences(length));
        }

        private Sequences GetAvailableHorizontalSequences(int length)
        {
            return GetAvailableSequences(Rows, Columns, (a, b) => squares[a, b], length);
        }
        private Sequences GetAvailableVerticalSequences(int length)
        {
            return GetAvailableSequences(Columns, Rows, (a, b) => squares[b, a], length);
        }

        private bool IsAvailable(Square square)
        {
            return square != null && square.SquareState == SquareState.Initial;
        }

        private Sequences GetAvailableSequences(int outerLoopLimit, int innerLoopLimit, SqaureAccess squareAccess, int length)
        {
            var result = new List<SquareSequence>();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (IsAvailable(squareAccess(o, i)))
                    {
                        queue.Enqueue(squareAccess(o, i));
                        if (queue.Count == length)
                        {
                            result.Add(queue.ToArray());
                        }
                    }
                    else
                    {
                        queue.Clear();
                    }
                }
            }
            return result;
        }

        public void MarkSquare(int row, int column, HitResult hitResult)
        {
            squares[row, column].Mark(hitResult);
        }

        public SquareSequence GetAvailableSequence(Square from, Direction direction)
        {
            int row = from.Row;
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
                endRow = Rows;
            }
            int column = from.Column;
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
                endColumn = Columns;
            }
            var result = new List<Square>();
            while (row != endRow || column != endColumn)
            {
                if (squares[row, column].SquareState != SquareState.Initial)
                {
                    break;
                }
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }

        public void Eliminate(int row, int column)
        {
            squares[row, column].Eliminate();
        }
    }   public class RecordGrid
    {
        public RecordGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public void RemoveSquares(SquareSequence squaresToRemove)
        {
            foreach (var square in squaresToRemove)
            {
                RemoveSquare(square.Row, square.Column);
            }
        }

        public Sequences GetAvailableSequences(int length)
        {
            var result = GetAvailableHorizontalSequences(length);
            if (length == 1)
            {
                return result;
            }
            return result.Concat(GetAvailableVerticalSequences(length));
        }

        private Sequences GetAvailableHorizontalSequences(int length)
        {
            return GetAvailableSequences(Rows, Columns, (a, b) => squares[a, b], length);
        }
        private Sequences GetAvailableVerticalSequences(int length)
        {
            return GetAvailableSequences(Columns, Rows, (a, b) => squares[b, a], length);
        }

        private bool IsAvailable(Square square)
        {
            return square != null && square.SquareState == SquareState.Initial;
        }

        private Sequences GetAvailableSequences(int outerLoopLimit, int innerLoopLimit, SqaureAccess squareAccess, int length)
        {
            var result = new List<SquareSequence>();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (IsAvailable(squareAccess(o, i)))
                    {
                        queue.Enqueue(squareAccess(o, i));
                        if (queue.Count == length)
                        {
                            result.Add(queue.ToArray());
                        }
                    }
                    else
                    {
                        queue.Clear();
                    }
                }
            }
            return result;
        }

        public void MarkSquare(int row, int column, HitResult hitResult)
        {
            squares[row, column].Mark(hitResult);
        }

        public SquareSequence GetAvailableSequence(Square from, Direction direction)
        {
            int row = from.Row;
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
                endRow = Rows;
            }
            int column = from.Column;
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
                endColumn = Columns;
            }
            var result = new List<Square>();
            while (row != endRow || column != endColumn)
            {
                if (squares[row, column].SquareState != SquareState.Initial)
                {
                    break;
                }
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }

        public void Eliminate(int row, int column)
        {
            squares[row, column].Eliminate();
        }
    }
}
