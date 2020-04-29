using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public readonly IEnumerable<Square> Squares;

        public ShipHitResult Hit(Square square)
        {
            if (!Squares.Contains(square))
            {
                return ShipHitResult.Missed;
            }

            Squares.First(s => s == square).Hit = true;
            if (Squares.Count(s => s.Hit) == Squares.Count())
            {
                foreach (var s in Squares)
                {
                    s.SetState(ShipHitResult.Sunken);
                }

                return ShipHitResult.Sunken;
            }

            return ShipHitResult.Hit;
        }
    }
}
