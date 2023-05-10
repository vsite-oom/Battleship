using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        private readonly List<Ship> ships = new();
        public IEnumerable<Ship> Ships { get { return ships; } }

        public void createShip(IEnumerable<Square> shipSquares)
        {
            ships.Add(new Ship(shipSquares));
        }

        public HitResult Fire(Square target)
        {
            foreach (var ship in ships) {
                var result = ship.Fire(target));
                if (result != HitResult.Hit)
                {
                    return result ;
                }
            }
            return HitResult.Missed;
        }
    }

}
