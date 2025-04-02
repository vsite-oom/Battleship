using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

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

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                squares[row, column] = new Square(row, column);
            }
        }
    }

    public IEnumerable<Square> Squares
    {
        get { return squares.Cast<Square>().Where(s => s != null); }
    }
}
