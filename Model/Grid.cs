using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using Placement = IEnumerable<Square>;
    public class Grid
    {

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

        public readonly int Rows;
        public readonly int Columns;

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
                    if (squares[r, c] != null)
                    {
                        result.Add(new List<Square> { squares[r, c] });
                    }
                }
            }
            return GeAvailableHorizontalPlacements(length).Concat(GetAvailableVerticalPlacements(length));
        }

        private IEnumerable<Placement> GeAvailableHorizontalPlacements(int length)
        {
            var result = new List<List<Square>>();
            for (int r = 0; r < Rows; r++)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; c++)
                {
                    if (squares[r, c] != null)
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
                    if (squares[r, c] != null)
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

        public void EliminateSqure(Placement toEliminate)
        {
            foreach (var item in toEliminate)
            {
                squares[item.Row, item.Column] = null;
            }
        }

        private Square[,] squares;
    }
}
