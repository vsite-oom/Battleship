using System;
using System.Collections;
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

        private readonly Square[,] squares;

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public Sequences GetAvailableSquences(int lenght)
        {
            var result = GetAvailableHorizontalSequences(lenght);
            if (lenght == 1) {
                return result;
            }
            return result.Concat(GetAvailableVerticalSequences(lenght));
        }

        private Sequences GetAvailableSequences(int outerLoopMax, int innerLoopMax, SquareAccess squareAccess, int lenght)
        {
            // ako nema dovoljno polja iza trenutne provjere onda break
            List<SquareSequence> result = new();
            for (int o = 0; o < outerLoopMax; ++o)
            {
                LimitedQueue<Square> queue = new(lenght);
                for (int i = 0; i < innerLoopMax; ++i)
                {
                    if (squareAccess(o, i) != null)
                    {
                        queue.Enqueue(squareAccess(0, i));
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
        private Sequences GetAvailableHorizontalSequences(int lenght)
        {
            return GetAvailableSequences(Rows, Columns, (a, b) => squares[a, b], lenght);
        }

        private Sequences GetAvailableVerticalSequences(int lenght)
        {
            return GetAvailableSequences(Columns, Rows, (a, b) => squares[b, a], lenght);
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
    }
}
