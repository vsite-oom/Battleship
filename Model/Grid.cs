using System;
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
        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] _squares;

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _squares = new Square[Rows, Columns];
            InitializeSquares();
        }

        private void InitializeSquares()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _squares[i, j] = new Square(i, j);
                }
            }
        }

        public SquareSequence AvailableSquares() => _squares.Cast<Square>().ToList();

        public void RemoveSquare(int row, int column) => _squares[row, column] = null;

        public Sequences GetAvailableSequences(int length)
        {
            return GetAvailableHorizontalSequances(length).Concat(GetAvailableVerticalSequances(length));
        }

        private Sequences GetAvailableHorizontalSequances(int length)
        {
            var result = new List<SquareSequence>();
            for (int r = 0; r < Rows; ++r)
            {
                var counter = 0;

                for (int c = 0; c < Columns; ++c)
                {
                    if (_squares[r, c] != null)
                    {
                        ++counter;
                        
                        if (counter >= length)
                        {
                            var toAdd = new List<Square>();

                            for (int cc = c - length + 1; cc <= c; ++cc)
                            {
                                toAdd.Add(_squares[r, cc]);
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

        private Sequences GetAvailableVerticalSequances(int length)
        {
            var result = new List<SquareSequence>();
            for (int c = 0; c < Columns; ++c)
            {
                var counter = 0;

                for (int r = 0; r < Rows; ++r)
                {
                    if (_squares[r, c] != null)
                    {
                        ++counter;

                        if (counter >= length)
                        {
                            var toAdd = new List<Square>();

                            for (int rr = c - length + 1; rr <= c; ++rr)
                            {
                                toAdd.Add(_squares[c, rr]);
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
    }
}
