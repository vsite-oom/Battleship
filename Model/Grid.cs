using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    using Placement = IEnumerable<Square>;
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        private readonly Square[,] squares;

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[rows, columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public void MarkHitResult(Square lastTarget, ShipHitResult hitResult)
        {
            squares[lastTarget.Row, lastTarget.Column].SetState(hitResult);
        }

        public IEnumerable<Placement> GetAvailablePlacements(int length)
        {
            if (length != 1)
            {
                return GeAvailableHorizontalPlacements(length).Concat(GetAvailableVerticalPlacements(length));
            }
            var result = new List<List<Square>>();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (IsAvailable(r, c))
                    {
                        result.Add(new List<Square> { squares[r, c] });
                    }
                }
            }
            return GeAvailableHorizontalPlacements(length).Concat(GetAvailableVerticalPlacements(length));
        }

        public void EliminateSquares(Placement toEliminate)
        {
            foreach (var item in toEliminate)
            {
                squares[item.Row, item.Column] = null;
            }
        }

        public IEnumerable<Square> GetSquaresNextTo(Square square, Direction direction)
        {
            List<Square> result = new List<Square>();
            int row = square.Row;
            int column = square.Column;
            int deltaRow = 0;
            int deltaCol = 0;
            int maxCount = 0;
            switch (direction)
            {
                case Direction.Right:
                    ++column;
                    deltaCol = +1;
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
                    deltaCol = -1;
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

            for (int i = 0; i < maxCount && IsAvailable(row, column); ++i)
            {
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaCol;
            }
            return result;
        }

        public IEnumerable<IEnumerable<Square>> GetSquaresInline(IEnumerable<Square> squaresHit)
        {
            List<Placement> result = new List<Placement>();
            //Horizontal
            if (squaresHit.First().Row == squaresHit.Last().Row)
            {
                var l = GetSquaresNextTo(squaresHit.First(), Direction.Left);
                if (l.Count() > 0)
                    result.Add(l);

                l = GetSquaresNextTo(squaresHit.Last(), Direction.Right);
                if (l.Count() > 0)
                    result.Add(l);
            }
            //Vertical
            else if (squaresHit.First().Column == squaresHit.Last().Column)
            {
                var l = GetSquaresNextTo(squaresHit.First(), Direction.Up);
                if (l.Count() > 0)
                    result.Add(l);

                l = GetSquaresNextTo(squaresHit.Last(), Direction.Down);
                if (l.Count() > 0)
                    result.Add(l);
            }
            else
            {
                Debug.Assert(false);
            }

            return result;
        }

        private IEnumerable<Placement> GeAvailableHorizontalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int r = 0; r < Rows; r++)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; c++)
                {
                    if (IsAvailable(r, c))
                    {
                        passed.Enqueue(squares[r, c]);
                    }
                    else
                    {
                        passed.Clear();
                    }
                    if (passed.Count == length)
                    {
                        result.Add(passed.ToList());
                    }
                }
            }
            return result;
        }

        private IEnumerable<Placement> GetAvailableVerticalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; c++)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; r++)
                {
                    if (IsAvailable(r, c))
                    {
                        passed.Enqueue(squares[r, c]);
                    }
                    else
                    {
                        passed.Clear();
                    }
                    if (passed.Count == length)
                    {
                        result.Add(passed.ToList());
                    }
                }
            }
            return result;
        }

        private bool IsAvailable(int row, int column)
        {
            return squares[row, column] != null && squares[row, column].SquareState == SquareState.None;
        }
    }
}
