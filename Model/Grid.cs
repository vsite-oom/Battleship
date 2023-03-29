﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<Square>;
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
            return squares.Cast<Square>();
        }
        public Sequences GetAvailableSequences(int length)
        {
            return GetAvailableHorizontalSequences(length);
        }

        private Sequences GetAvailableHorizontalSequences(int length)
        {
            var result = new List<SquareSequence>();
            for (int r =0; r< Rows; ++r)
            {
                int counter = 0;
                for (int c =0; c < Columns; ++c)
                {
                    if (squares[r,c] != null)
                    {
                        ++counter;
                        if (counter >= length)
                        {
                            var toAdd = new List<Square>();
                            for (int cc = c-length +1; cc<=c; ++cc)
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
            return (Sequences)result;
        }
        private Sequences GetAvailableVerticalSequences(int length)
        {
            var result = new List<SquareSequence>();
            return (Sequences)result;
        }

    }
}
