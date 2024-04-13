using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class Fleet
	{
		private List<Ship> ships = new List<Ship>();

		public IEnumerable<Ship> Ships { get {  return ships; } }

		public void CreateShip(List<Square> squares)
		{
			var ship=new Ship(squares);
			ships.Add(ship);
		}
	}
}
