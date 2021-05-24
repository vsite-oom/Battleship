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

        public HitResult Hit(Square square)
        {
            if (squares.Contains(square) == false)
                return HitResult.Missed;
            for (int i = 0; i < squares.Length; ++i)
            {
                if (squares[i].Equals(square))
                {
                    squares[i].SetSquareState(HitResult.Hit);
                    break;
                }

            }
            if (squares.All(s => s.SquareState == SquareState.Hit))
            {
                for (int i = 0; i < squares.Length; ++i)
                {
                    squares[i].SetSquareState(HitResult.Sunken);
                }
                return HitResult.Sunken;
            }
            return HitResult.Hit;
        }

        private readonly Square[] squares;
    }
}
