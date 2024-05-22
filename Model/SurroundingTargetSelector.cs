using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class SurroundingTargetSelector : ITargetSelector
	{
		public SurroundingTargetSelector(ShotsGrid grid, Square firstHit, int shipLength)
		{
			this.grid = grid;
			this.firstHit = firstHit;
			this.shipLength = shipLength;
		}
		private readonly ShotsGrid grid;
		private readonly Square firstHit;
		private readonly int shipLength;

		public Square Next()
		{
			List<IEnumerable<Square>> squares = new List<IEnumerable<Square>>();
			var up = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, ShotsGrid.Direction.Upwards);
			if (up.Count() > 0) { squares.Add(up); }
			var right = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, ShotsGrid.Direction.Rightwards);
			if (right.Count() > 0) { squares.Add(right); }
			var down = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, ShotsGrid.Direction.Downwards);
			if (down.Count() > 0) { squares.Add(down); }
			var left = grid.GetSquaresInDirection(firstHit.Row, firstHit.Column, ShotsGrid.Direction.Leftwards);
			if (left.Count() > 0) { squares.Add(left); }

			throw new NotImplementedException();
		}
	}
}
