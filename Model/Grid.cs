using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsie.Oom.Battleship.Model
{
    public class Grid
    {
        public Grid(int rows, int columns)
        {
            rows = rows;
            columns = columns;
            squares = new Square[Rows, Columns];
            for(int r = 0; r< rows; r++)
            {
                for (int c = 0; c < Columns; ++c){
                    squares[r,c]= new Square(r,c); 
            }
            }
        }
        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public IEnumerable<Square> AvaliableSquares()
        {
            return squares.Cast<Square>();
            throw new NotImplementedException();
        }
    }
}
