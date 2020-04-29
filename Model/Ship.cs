using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public enum HitResult
    {
        Missed,
        Hit,
        Sunk
    }

    public class Ship
    {
        public readonly IEnumerable<Square> Squares;

        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public HitResult IsHit(Square square)
        {
            if (!Squares.Contains(square))
            {
                
                return HitResult.Missed;
            }
            Squares.First(s => s == square).Hit = true;
            if (Squares.Count(s => s.Hit) == Squares.Count())
            {
                foreach (var s in Squares)
                    s.SetState(HitResult.Sunk);
                return HitResult.Sunk;
            }
            
            return HitResult.Hit;
            
        }
    }
}
