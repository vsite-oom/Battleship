using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Square
    {
        public readonly int Row;
        public readonly int Column;
        private bool hit = false;
        public bool IsHit => hit;

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Hit()
        {
            hit = true;
        }
    }
}
