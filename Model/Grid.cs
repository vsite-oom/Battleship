﻿using System;
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

    public enum Direction
    {
        Upwards,
        Downwards,
        Leftwards,
        Rightwards
    }

    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        private readonly Square[,] squares;

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

        public SquareSequence AvailableSquares()
        {
            return squares.Cast<Square>().Where(sq => sq != null);
        }

        public Sequences GetAvailableSequences(int lenght)
        {
            var result = GetAvailableHorizontalSequences(lenght);
            if (lenght == 1) {
                return result;
            }
            return result.Concat(GetAvailableVerticalSequences(lenght));
        }

        private Sequences GetAvailableSequences(int outerLoopMax, int innerLoopMax, SquareAccess squareAccess, int lenght)
        {
            List<SquareSequence> result = new();
            for (int o = 0; o < outerLoopMax; ++o)
            {
                LimitedQueue<Square> queue = new(lenght);
                for (int i = 0; i < innerLoopMax; ++i)
                {
                    if (squareAccess(o, i) != null)
                    {
                        queue.Enqueue(squareAccess(o, i));
                        if (queue.Count == lenght)
                        {
                            result.Add(queue.ToArray());
                        }
                    }
                    else
                    {
                        queue.Clear();
                        if (lenght > innerLoopMax - i)
                            break;
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

        public void RemoveSquares(SquareSequence squaresToRemove)
        {
            foreach (var square in squaresToRemove)
            {
                RemoveSquare(square.Row, square.Column);
            }
        }

        public SquareSequence GetAvailableSequence(Square reference, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}
