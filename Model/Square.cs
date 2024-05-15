﻿using System;
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
            State = SquareState.Intact;
        }

        public readonly int Row;
        public readonly int Column;

        public void Hit()
        {
            SquareState = SquareState.Hit;
        }

        public void ChangeState(SquareState newState)
        {
          if ((int)newState > (int)SquareState) {

                SquareState = newState;
            }
           
        }

        public bool IsHit => (int)SquareState >= (int)SquareState.Hit;


        public SquareState State {get; private set;}
    }
}
