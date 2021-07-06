using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public void CreateShip(IEnumerable<Square> squares)
        {
            ships.Add(new Ship(squares));
        }

        public bool AreAllSunken()
        {
            return ships.All( s => s.Squares.All( sq => sq.SquareState == SquareState.Sunken));
        }

        public IEnumerable<Ship> Ships { get { return ships; } }

        private List<Ship> ships = new List<Ship>();

        public HitResult Hit(Square square)
        {
            foreach (var ship in ships)
            {
                var hitResult = ship.Hit(square);

                if (hitResult != HitResult.Missed)
                {
                    return hitResult;
                }
            }

            return HitResult.Missed;
        }
    }
}
