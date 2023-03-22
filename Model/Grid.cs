using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{

    public class Grid
    {
        readonly public int rows;
        readonly public int columns;
        private Square[,] squares;
        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            squares = new Square[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < columns; c++)
                {
                    squares[r, c]=new Square(r, c);
                    
                }
            }
        }
        public IEnumerable<Square> AvailableSquares()
        {
            return squares.Cast<Square>();
        }
    }
}
