using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class fleet
    {
        public void addShip(IEnumerable<Square> squares)
        {
            ships.Add(new ship(squares));
        }
        public IEnumerable<ship> Ships
        {
            get { return ships; }
        }
        public HitResult Hit(Square square)
        {
            foreach (var ship in ships)
            {
                HitResult hit = ship.Hit(square);
                if (hit != HitResult.Missed)
                {
                    return hit;
                }

            }
            return HitResult.Missed;
        }
        public int getNumberOfShips()
        {
            return Ships.Count();
        }
        private List<ship> ships = new List<ship>();
    }
}
