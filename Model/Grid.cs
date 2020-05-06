using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    using Placement = IEnumerable<Square>;

   public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            this.squares = new Square[this.Rows, this.Columns];
            for (int r = 0; r < this.Rows; ++r)
            {
                for (int c = 0; c < this.Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public void MarkHitResult(Square square, HitResult hitResult)
        {

        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {
            if(length != 1)
            {
                return GetAvailableHorizontalPlacement(length).Concat(GetAvailableVerticalPlacement(length));
            }
            
            List<List<Square>> result = new List<List<Square>>();
            for (int r = 0; r < this.Rows; ++r)
            {
                for (int c = 0; c < this.Columns; ++c)
                {
                  if(IsAvailable(r,c))
                  {
                     result.Add(new List<Square> { squares[r, c] });
                  }

                }
                
            }
            return result;

        }

        public void EliminateSquares(Placement eliminate)
        {
            foreach(var square in eliminate)
            {
                squares[square.row, square.column] = null;
            }

            
            
        }

        private IEnumerable<Placement> GetAvailableVerticalPlacement(int length)
        {
            var result = new List<List<Square>>();
            for (int c = 0; c < Columns; ++c)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length);
                for (int r = 0; r < Rows; ++r)
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

        private IEnumerable<Placement> GetAvailableHorizontalPlacement(int length)
        {
            var result = new List<List<Square>>();
            for(int r = 0; r < Rows; ++r)
            {
                LimitedQueue<Square> passed = new LimitedQueue<Square>(length); 
                for(int c = 0; c < Columns; ++c)
                {
                    if(IsAvailable(r,c))
                    {
                        passed.Enqueue(squares[r, c]);
                    }
                    else
                    {
                        passed.Clear();
                    }
                    if(passed.Count == length)
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
