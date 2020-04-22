using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();

        public void AddShip(IEnumerable<Square> squares)
        {
            ships.Add(new Ship(squares));
        }

        public HitResult Hit(Square square)
        {
            foreach(Ship ship in ships)
            {
                HitResult hit = ship.IsHit(square);
                if (hit != HitResult.Missed)
                    return hit;
            }

            return HitResult.Missed;
        }

        public IEnumerable<Ship> Ships
        {
            get { return ships; }
        }
    }
}
