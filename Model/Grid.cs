using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

class Grid
{
    public readonly int Rows;
    public readonly int Columns;

    private readonly Square[,] squares;

    public Grid(int rows, int columns, Square[,] squares)
    {
        Rows = rows;
        Columns = columns;
        this.squares = squares;

        for(int row = 0; row<Rows; row++)
        {
            for(int column=0; column<Columns; column++)
            {
                squares[row, column] = new Square(row, column);
            }
        }
    }

    public IEnumerable<Square> Squares()
    {
        get{return squares.Cast<Square>().Where(s=>s!=null) }
    }
}
