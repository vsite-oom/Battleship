using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public Sequences GetAvailableSquences(int lenght)
        {
            var result = GetAvailableHorizontalSequences(lenght);
            if (lenght == 1)
            {
                return result;
            }
            return GetAvailableHorizontalSequences(lenght).Concat(GetAvailableVerticalSequences(lenght));
        }

        private Sequences GetAvailableHorizontalSequences(int lenght)
        {
            List<SquareSequence> result = new();
            for (int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> queue = new(lenght);
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                    {
                        queue.Enqueue(squares[r, c]);
                        if (queue.Count == lenght)
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

        private Sequences GetAvailableVerticalSequences(int lenght)
        {
            List<SquareSequence> result = new();
            for (int c = 0; c < Columns; ++c)
            {
                LimitedQueue<Square> queue = new(lenght);
                for (int r = 0; r < Rows; ++r)
                {
                    if (squares[r, c] != null)
                    {
                        queue.Enqueue(squares[r, c]);
                        if (queue.Count == lenght)
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
        private Sequences GetAvailableSequences(int outerLoopLimit, int innerLoopLimit, SquareAccess squareAccess, int lenght)
        {
            List<SquareSequence> result = new();
            for (int o = 0; o < outerLoopLimit; ++o)
            {
                LimitedQueue<Square> queue = new(lenght);
                for (int i = 0; i < innerLoopLimit; ++i)
                {
                    if (squaresAccess(o, i) != null && squareAccess(o, i).SquareState == SquareState.Initial)
                    {
                        queue.Enqueue(squares[o, i]);
                        if (queue.Count == lenght)
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

        private object squaresAccess(int o, int i)
        {
            throw new NotImplementedException();
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

        public void MarkSquare(int row, int column, HitResult hitResult)
        {
            squares[row, column].Mark(hitResult);
        }
    }
}
