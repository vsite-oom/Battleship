using System.Collections.Generic;
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

        public void EliminateSqure(Placement toEliminate)
        {
            foreach (var item in toEliminate)
            {
                squares[item.Row, item.Column] = null;
            }
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
