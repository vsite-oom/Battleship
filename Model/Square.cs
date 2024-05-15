using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Intact,
        Eliminated,
        Missed,
        Hit,
        Sunken
    }
    public class Square
    {
        public Square(int row, int column) 
        {
            Row = row;
            Column = column;
        }

        public readonly int Row;
        public readonly int Column;

        public void Hit() 
        {
            hit = true;
        }

        public bool IsHit => hit;

        private bool hit = false;


    }
}
