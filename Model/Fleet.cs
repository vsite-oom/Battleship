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

        public void createShip(IEnumerable<Square> shipSquares)
        {
            ships.Add(new Ship(shipSquares));
        }

        public IEnumerable<Ship> Ships { get { return ships; } }

    }

}
