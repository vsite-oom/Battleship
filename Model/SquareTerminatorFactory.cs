using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	public enum ShipAdjoining
	{
		None,
		Corners
	}
	public static class SquareTerminatorFactory
	{
		public static ISquareTerminator Create(ShipAdjoining adjoining, int rows, int cols)
		{
			switch(adjoining)
			{
				case ShipAdjoining.None:
					return new SquareTerminator(rows, cols);
				case ShipAdjoining.Corners:
					return null;
				default:
					Debug.Assert(false);
					return null;
			}
				
		}
	}
}
