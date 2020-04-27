using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Model
{
    using Placment = IEnumerable<Square>;
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
        public IEnumerable<Placment> GetAvailablePlacements(int length)
        {
            if (length != 1)
                return GetAvailableHorizontalPlacement(length).Concat(GetAvailableVerticalPlacement(length));

            List<List<Square>> result = new List<List<Square>>();
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                        result.Add(new List<Square> { squares[r, c] });
                }
            }
            return result;


        }

        public void EliminateSquares(Placment ToEliminate)
        {
            foreach (var square in ToEliminate)
                squares[square.Row, square.Column] = null;

        }

        private IEnumerable<Placment> GetAvailableHorizontalPlacement(int length)
        {
            var result = new List<List<Square>>();
            for (int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                        passed.Enqueue(squares[r, c]);
                    else
                        passed.Clear();

                    if (passed.Count == length)
                    {
                        result.Add(passed.ToList());
                    }
                }
            }
            return result;

        }
        private IEnumerable<Placment> GetAvailableVerticalPlacement(int length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; ++r)
                {
                    if (squares[r, c] != null)
                        passed.Enqueue(squares[r, c]);
                    else
                        passed.Clear();

                    if (passed.Count == length)
                    {
                        result.Add(passed.ToList());
                    }
                }
            }
            return result;
        }

        public readonly int Rows;
        public readonly int Columns;
        private Square[,] squares;
    }
}



