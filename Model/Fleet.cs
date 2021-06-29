using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public int RemainingShipNumber;
        public void CreateShip(IEnumerable<Square> squares)
        {
            ships.Add(new Ship(squares));
        }

        public IEnumerable<Ship> Ships
        {
            get { return ships; }
        }

        private List<Ship> ships = new List<Ship>();

        public HitResult Hit(Square square)
        {
            foreach (Ship ship in ships)
            {
                var result = ship.Hit(square);
                if (result != HitResult.Missed)
                    return result;
            }
            return HitResult.Missed;
        }

        public Ship shipOnSquare(Square sq)
        {
            return ships.Find(s => s.Squares != null && s.Squares.Contains(sq));
        }
    }
}
