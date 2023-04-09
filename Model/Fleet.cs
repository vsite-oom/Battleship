using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public List<Ship> ships = new List<Ship>();

        public void CreateShip(IEnumerable<Square> squares)
        {

            ships.Add(new Ship(squares));
        }
        public IEnumerable<Ship> Ships => ships;


    }
}
