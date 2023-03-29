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
            return squares.Cast<Square>();
        }

        public Sequences GetAvailableSquences(int lenght)
        {
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
            return result;
        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }
    }
}
