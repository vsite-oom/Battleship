using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum SquareState
    {
        Intact,
        Eliminated,
        Miss,
        Hit,
        Sunk
    }
    public class Square
    {
        public readonly int Row;
        public readonly int Column;
        public SquareState State {  get; private set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            State = SquareState.Intact;
        }
        
        public bool IsHit => (int)State>=(int)SquareState.Hit;
        public void Hit()
        {
            State=SquareState.Hit;
        }
        public void changeState(SquareState newState)
        {
            if ((int)newState>(int)State) {
            State=newState;
            }
        }
    }
}
