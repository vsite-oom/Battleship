using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public readonly IEnumerable<Square> Squares;

        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public HitResult Fire(Square target)
        {
            var found = Squares.FirstOrDefault(s => s == target);
            if(found == null)
            {
                return HitResult.Missed;
            }
            found.Mark(HitResult.Hit);
            if(Squares.All(s => s.State == Square.SquareState.Hit))
            {
                foreach(var square in Squares) { square.Mark(HitResult.Sank); }
                
                return HitResult.Sank;
            }
            return HitResult.Hit;
        }
    }
}
