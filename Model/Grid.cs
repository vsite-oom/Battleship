using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using static Vsite.Oom.Battleship.Model.Ship;
    using Placement = IEnumerable<Square>;
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;

        private Square[,] squares;
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[rows, columns];

            for(int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public void EliminateSquares(Placement toEliminate)
        {
            foreach (var square in toEliminate)
                squares[square.Row, square.Column] = null;
        }

        public IEnumerable<Placement> GetAvailablePlacements(int length)
        {
            if (length != 1)
            {
                return GetAviableHorizontalPlacements(length).Concat(GetAvailableVerticalPlacements(length));
            }

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

        private IEnumerable<Placement> GetAviableHorizontalPlacements (int length)
        {
            var result = new List<List<Square>>();
            for(int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for(int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                        passed.Enqueue(squares[r, c]);
                    else
                        passed.Clear();

                    if(passed.Count() == length)                   
                        result.Add(passed.ToList());
                   
                }
            }
            return result;
        }

        private IEnumerable<Placement> GetAvailableVerticalPlacements(int Length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(Length);
                for (int r = 0; r < Rows; ++r)
                {
                    if (squares[r, c] != null)                    
                        passed.Enqueue(squares[r, c]);                    
                    else                    
                        passed.Clear();
                   
                    if (passed.Count == Length)                   
                        result.Add(passed.ToList());
                   
                }
            }
            return result;
        }

        public void MarkHitResult(Square square, HitResult hitResult)
        {

        }
    }
}
