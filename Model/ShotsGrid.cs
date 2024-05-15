using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class ShotsGrid : Grid
	{
		public ShotsGrid(int rows, int columns):base(rows,columns) 
		{ 

		}
		protected override bool IsSquareAvailable(int row, int column)
		{
			throw new NotImplementedException();
		}
	}
}
