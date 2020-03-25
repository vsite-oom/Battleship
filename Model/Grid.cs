using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vsite.Oom.Battleship.Model
{
  public  class Grid
    {
        public Grid(int rows,int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for(int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }
        public void EliminateSquares(IEnumerable<Square> squares )
        {
            throw new NotImplementedException();
        }
       public  IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {   
           throw new NotImplementedException();
        }
        public readonly int Rows;
        public readonly int Columns;
        private Square[,] squares;
    }
}
