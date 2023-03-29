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
                int counter = 0;
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                    {
                        ++counter;
                        if (counter >= lenght)
                        {
                            List<Square> toAdd = new();
                            for (int cc = c - lenght + 1; cc <= c; ++cc)
                            {
                                toAdd.Add(new Square(r, cc));
                            }
                            result.Add(toAdd);
                        }
                    }
                    else
                    {
                        counter = 0;
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
