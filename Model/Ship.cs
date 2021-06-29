using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum HitResult
    {
        Missed,
        Hit,
        Sunken
    }

    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares.ToArray();
        }

        public IEnumerable<Square> Squares
        {
            get { return squares; }
        }

        public HitResult Hit(Square target)
        {
            if (!squares.Contains(target))
                return HitResult.Missed;
            for (int i = 0; i < squares.Length; ++i)
            {
                if (squares[i].Equals(target))
                    squares[i].SetState(SquareState.Hit);
            }
            if (squares.All(s => s.SquareState == SquareState.Hit || s.SquareState == SquareState.Sunken))
            {
                for (int i = 0; i < squares.Length; ++i)
                {
                    if (squares[i].Equals(target))
                        squares[i].SetState(SquareState.Sunken);
                }
                return HitResult.Sunken;
            }
            return HitResult.Hit;
        }

        private Square[] squares = null;
    }
}
