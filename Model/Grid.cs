using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public abstract class Grid
	{
		protected Grid(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;

			squares = new Square[Rows, Columns];

			for (int r = 0; r < Rows; r++)
			{
				for (int c = 0; c < Columns; c++)
				{
					squares[r, c] = new Square(r, c);
				}
			}
		}

		public readonly int Rows;
		public readonly int Columns;

		protected readonly Square?[,] squares;

		public virtual IEnumerable<Square> Squares
		{
			get { return squares.Cast<Square>(); }
		}

		public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
		{
			return GetHorizontalAvailablePlacemets(length);
		}

		protected abstract bool IsSquareAvailable(int row, int column);

		private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacemets(int length)
		{
			List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

			for (int r = 0; r < Rows; r++)
			{
				int counter = 0;
				for (int c = 0; c < Columns; c++)
				{
					if (IsSquareAvailable(r,c))
					{
						++counter;
						if (counter >= length)
						{
							List<Square> temp = new List<Square>();
							for (int c1 = c - length + 1; c1 <= c; ++c1)
							{
								temp.Add(squares[r, c1]!);
							}
							result.Add(temp);
						}
					}
					else
					{
						counter = 0;
					}
				}

			}
			return result;
		}

	}
}
