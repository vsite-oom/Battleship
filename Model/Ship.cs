using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public readonly IEnumerable<Square> squares;
        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares;
        }

        public HitResult Fire(Square target)
        {
            var found = squares.FirstOrDefault(x => x == target);
            if (found == null)
            {
                return HitResult.Missed;
            }
            found.Mark(HitResult.Hit);
            if(squares.All(x => x.squareState == SquareState.Hit))
            {
                foreach(var square in squares)
                {
                    square.Mark(HitResult.Sunk);
                }
                return HitResult.Sunk;
            }
            else
            {
                return HitResult.Hit;
            }
            
        }
    }
}
