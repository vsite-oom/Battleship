using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public enum HitResult
    {
        Missed, 
        Hit,
        Sunken
    };

    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares.ToArray();
        }

        public IEnumerable<Square> Squares
        {
            get { return squares;  }
        }

        public HitResult Hit(Square square)
        {
            int index = Array.FindIndex(squares, item => item.Equals(square));
            if (index == -1)
            {
                square.SetSquareState(HitResult.Missed);
                return HitResult.Missed;
            }

            squares[index].SetSquareState(HitResult.Hit);
            if (squares.All(item => item.SquareState == SquareState.Hit))
            {
                for (int i = 0; i < squares.Length; i++)
                {
                    squares[i].SetSquareState(HitResult.Sunken);
                }

                return HitResult.Sunken;
            }

            return HitResult.Hit;
        }


        private Square[] squares;
    }
}
