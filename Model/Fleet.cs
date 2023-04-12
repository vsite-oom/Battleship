using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    internal class Fleet
    {
        public void CreateShip(IEnumerable<Square> shipSquares)
        {
            ships.Add(new Ship((IEnumerable<Ship>)shipSquares));
        }
        private List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships => ships;
    }
    
}
