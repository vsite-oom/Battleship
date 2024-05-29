using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class ShotsGrid : Grid
	{
		public enum Direction
		{
			Upwards,
			Rightwards,
			Leftwards,
			Downwards
		}
		public ShotsGrid(int rows, int columns):base(rows,columns) 
		{ 

		}
		protected override bool IsSquareAvailable(int row, int column)
		{
			return squares[row, column]?.squareState == SquareState.Intact;
		}

		public Square GetSquare(int row, int column)
		{
			return squares[row, column]!;
		}

		public void ChangeSquareState(int row, int column, SquareState newState)
		{
			squares[row, column]!.ChangeState(newState);
		}

		public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction upwards)
		{
			throw new NotImplementedException();
		}
	}
}
