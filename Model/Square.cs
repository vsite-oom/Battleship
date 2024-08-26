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

        public void Hit()  // Metoda za označavanje polja kao pogodak.
        {
            SquareState = SquareState.Hit;
        }

        public void ChangeState(SquareState newState)  // Stanje polja se mijenja samo ako je novo stanje više od trenutnog.
        {
            if ((int)newState > (int)SquareState)
            {
                SquareState = newState;
            }
        }

        public bool IsHit => (int)SquareState >= (int)SquareState.Hit;

        public SquareState SquareState { get; private set; }
    }
}
