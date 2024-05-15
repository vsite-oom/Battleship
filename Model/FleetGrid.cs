using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class FleetGrid : Grid
	{
		public FleetGrid(int rows, int columns) : base(rows, columns)
		{

		}

		public override IEnumerable<Square> Squares
		{
			get { return squares.Cast<Square>().Where(s => s != null); }
		}

		public void EliminateSquare(int row, int column)
		{
			squares[row, column] = null;
		}

		protected override bool IsSquareAvailable(int row, int column)
		{
			return squares[row, column] != null;
		}
	}
}
