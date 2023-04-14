﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<IEnumerable<Square>>;
    using SquareAccess = Func<int, int, Square>;
    public class Grid
    {
        public Grid(int rows, int columns)
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

        private Square[,] squares;



        public IEnumerable<Square> AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        public void RemoveSquares(IEnumerable<Square> squaresToRemove)
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
            //var result = new List<SquareSequence>();
            //for (int r =0; r< Rows; ++r)
            //{
            //    var queue = new LimitedQueue<Square>(length);
            //    for (int c = 0; c < Columns; ++c)
            //    {
            //        if (squares[r,c] != null)
            //        {
            //            queue.Enqueue(squares[r, c]);
            //            if (queue.Count == length)
            //            {
            //                result.Add(queue.ToArray());
            //            }
            //        }
            //        else
            //        {
            //            queue.Clear();
            //        }
            //    }
            //}
            //return (Sequences)result;
        }
        private Sequences GetAvailableVerticalSequences(int length)
        {
            return GetAvailableSequences(Columns, Rows, (a,b) => squares[b,a], length);
            //var result = new List<SquareSequence>();
            //for (int c = 0; c < Columns; ++c)
            //{
            //    var queue = new LimitedQueue<Square>(length);
            //    for (int r = 0; r < Rows; ++r)
            //    {
            //        if (squares[r, c] != null)
            //        {
            //            queue.Enqueue(squares[r, c]);
            //            if (queue.Count == length)
            //            {
            //                result.Add(queue.ToArray());
            //            }
            //        }
            //        else
            //        {
            //            queue.Clear();
            //        }
            //    }
            //}
            //return result;
        }

        private Sequences GetAvailableSequences(int outerLoopLimit,int innerLoopLimit, SquareAccess squareAccess, int length)
        {
            var result = new List<SquareSequence>();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (squareAccess(o, i) != null)
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

    }
}
