using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Model
{
    internal class Square
    {
        public readonly int Row;
        public readonly int Column;

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
