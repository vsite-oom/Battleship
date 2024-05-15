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

		public void CreateShip(IEnumerable<Square> squares)
		{
			var ship=new Ship(squares);
			ships.Add(ship);
		}

		public hitResult Hit(int row, int column)
		{
			foreach (Ship ship in ships)
			{
				hitResult hitResult = ship.Hit(row, column);
				if (hitResult != hitResult.Missed)
					return hitResult;
			}
			return hitResult.Missed;
		}
	}
}
