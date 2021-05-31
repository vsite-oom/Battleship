using System.Collections.Generic;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public void CreateShip(IEnumerable<Square> squares)
        {
            Ship ship = new Ship(squares);
            ships.Add(ship);
        }
        public IEnumerable<Ship> Ships
        {
            get { return ships; }
        }
        public HitResult Hit(Square square)
        {
            foreach (Ship ship in ships)
            {
                var hit = ship.Hit(square);
                if (hit != HitResult.Missed)
                    return hit;
            }
            return HitResult.Missed;
        }
        private List<Ship> ships = new List<Ship>();
    }



}
