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
            SquareState = SquareState.Intact;
        }

        public readonly int Row;
        public readonly int Column;

        public void Hit() 
        {
            SquareState = SquareState.Hit;
        }

        public bool IsHit => (int)SquareState >= (int)SquareState.Hit;

        public SquareState SquareState { get; private  set; }
    }
}
