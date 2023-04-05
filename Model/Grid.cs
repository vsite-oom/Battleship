using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<IEnumerable<Square>>;
    using SquareAcess = Func<int, int, Square>;
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

        private readonly Square[,] squares;

        public IEnumerable<Square> AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }
        public Sequences GetAvailableSequences(int length)
        {
            var result = GetAvailableSequences(length);
            if (length == 1)
            {
                return result;
            }
            return GetAvailableHorizontalSequences(length).Concat(GetAvailableVerticalSequences(length));
        }
        private Sequences GetAvailableHorizontalSequences(int length)
        {
            return GetAvailabeSequences(Rows, Columns, (a, b) => squares[a, b], length);
            //var result = new List<SquareSequence>();
            //for (int r = 0; r < Rows; ++r)
            //{
            //    var queue = new LimitedQueue<Square>(length);
            //    for (int c = 0; c < Columns; ++c)
            //    {
            //        if (squares[r, c] != null)
            //        {
            //            queue.Enqueue(squares[r, c]);
            //            if (queue.Count == length)
            //            {
            //                result.Add(queue.ToArray());
            //                var toAdd = new List<Square>();
                           
            //            }
            //            else
            //            {

            //                queue.Clear();
            //            }
            //        }

            //    }
            //}
            //return result;
        }

        private Sequences GetAvailableVerticalSequences(int length)
        {
            return GetAvailabeSequences(Columns, Rows, (a,b) => squares[b,a], length);
            //var result = new List<SquareSequence>();
            //for (int r = 0; r < Columns; ++r)
            //{
            //    var queue = new LimitedQueue<Square>(length);
            //    for (int c = 0; c < Rows; ++c)
            //    {
            //        if (squares[r, c] != null)
            //        {
            //            queue.Enqueue(squares[r, c]);
            //            if (queue.Count == length)
            //            {
            //                result.Add(queue.ToArray());
            //                var toAdd = new List<Square>();

            //            }
            //            else
            //            {

            //                queue.Clear();
            //            }
            //        }

            //    }
            //}
            //return result;
        }
        
        private Sequences GetAvailabeSequences(int outerLoopLimit, int innerLoopLimit, SquareAcess squareAccess, int length)
        {
            var result = new List<SquareSequence>();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                var queue = new LimitedQueue<Square>(length);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (squareAccess(o,i) != null)
                    {
                        queue.Enqueue(squareAccess(o, i));
                        if (queue.Count == length)
                        {
                            result.Add(queue.ToArray());
                            var toAdd = new List<Square>();

                        }
                        else
                        {

                            queue.Clear();
                        }
                    }

                }
            }
            return result;
        }
    }
}
