using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class Square
	{
		public Square(int row, int column)
		{
			Row = row;
			Column = column;
		}
		public readonly int Row;
		public readonly int Column;
	}
}
