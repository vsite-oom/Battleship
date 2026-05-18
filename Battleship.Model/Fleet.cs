using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Model
{
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships { get { return ships; } }
    }
}
