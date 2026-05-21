using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model;

public class Square
{
    public readonly int Row;
    public readonly int Column;

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
    }
}