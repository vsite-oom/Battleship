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
    public class ship
    {
        public ship(IEnumerable<Square> squares) {
            this.squares = squares;
        }
        public HitResult Hit(Square square)
        {
            if (!squares.Contains(square))
            {
                return HitResult.Missed;
            }
            squares.First(s => s == square).Hit = true;
            if (squares.Count(s => s.Hit) == squares.Count())
            {
                foreach(var s in squares)
                {
                    s.SetState(HitResult.Sunken);
                }
                return HitResult.Sunken;
            }
            return HitResult.Hit;
        }
        public readonly IEnumerable<Square> squares;

    }

}
