using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using Placement = IEnumerable<Square>;

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

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
                    squares[r, c] = new Square(r, c);
            }
        }

        public IEnumerable<Placement> GetAvaiablePlacements(int length)
        {
            if (length != 1)
                return GetAvaiableHorizontalPlacements(length).Concat(GetAvaiableVerticalPlacements(length));
            List<List<Square>> result = new List<List<Square>>();
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    if (IsAvailable(r, c))
                        result.Add(new List<Square> { squares[r, c] });
                }
            }
            return result;
        }

        public void EliminateSquares(Placement toEliminate)
        {
            foreach (var square in toEliminate)
                squares[square.Row, square.Column] = null;
        }

        public void MarkHitResult(Square square, HitResult hitResult)
        {
            squares[square.Row, square.Column].SetState(hitResult);
        }

        public IEnumerable<Square> GetSquaresNextTo(Square square, Direction direction)
        {
            List<Square> result = new List<Square>();
            int row = square.Row;
            int column = square.Column;
            int deltaRow = 0;
            int deltaColumn = 0;
            int maxCount = 0;

            switch (direction)
            {
                case Direction.Right:
                    ++column;
                    deltaColumn = +1;
                    maxCount = Columns - column;
                    break;
                case Direction.Down:
                    ++row;
                    deltaRow = +1;
                    maxCount = Rows - row;
                    break;
                case Direction.Left:
                    maxCount = column;
                    --column;
                    deltaColumn = -1;
                    break;
                case Direction.Up:
                    maxCount = row;
                    --row;
                    deltaRow = -1;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            for (int i = 0; i < maxCount && IsAvailable(row,column); ++i)
            {
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }

            return result;
        }

        private IEnumerable<Placement> GetAvaiableHorizontalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for(int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for(int c = 0; c < Columns; ++c)
                {
                    if (IsAvailable(r, c))
                        passed.Enqueue(squares[r, c]);
                    else
                        passed.Clear();
                    if(passed.Count == length)
                        result.Add(passed.ToList());
                }
            }
            return result;
        }

        private IEnumerable<Placement> GetAvaiableVerticalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                int counter = 0;
                for (int r = 0; r < Rows; ++r) {
                    if (IsAvailable(r, c))
                        ++counter;
                    //passed.Enqueue(squares[r, c]);
                    else
                        counter = 0;
                    if (counter >= length)
                    {
                        List<Square> seq = new List<Square>();
                        for (int first = r - length + 1; first <= r; ++first)
                            seq.Add(squares[first, c]);
                        result.Add(seq);
                    }
                }
            }
            return result;
        }

        private bool IsAvailable(int row, int column)
        {
            return squares[row, column] != null && squares[row, column].SquareState == SquareState.None;
        }

        public readonly int Rows;
        public readonly int Columns;

        private Square[,] squares;
    }
}
