using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	using Placement = IEnumerable<Square>;
	public class Grid
	{

		public readonly int Rows, Columns;
		private Square[,] squares;

		public Grid(int rows, int columns)
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

		public IEnumerable<Placement> GetAvailablePlacements(int lenght)
		{
			if (lenght != 1)
			{
				return GetAwailableHorizontalPlacements(lenght).Concat(GetAwailableVerticalPlacements(lenght));
			}
			List<List<Square>> result = new List<List<Square>>();
			for (int r = 0; r < Rows; r++)
			{
				for (int c = 0; c < Columns; c++)
				{
					if (isAvailable(r,c))
						result.Add(new List<Square> { squares[r, c] });
				}
			}

			return result;
		}

		private bool isAvailable(int row, int column)
		{
			return squares[row, column] != null && squares[row, column].SquareState == SquareState.None;
		}

		public void EliminateSquares(Placement toEliminate)
		{
			foreach (var square in toEliminate)
			{
				squares[square.Row, square.Column] = null;
			}
		}


		public void MarkHitResult(Square square, HitResult hitResult)
		{
			squares[square.Row, square.Column].SetState(hitResult);
		}

		private IEnumerable<Placement> GetAwailableHorizontalPlacements(int lenght)
		{
			var result = new List<List<Square>>();

			for (int r = 0; r < Rows; r++)
			{
				LimitedQueue<Square> passed = new LimitedQueue<Square>(lenght);

				for (int c = 0; c < Columns; c++)
				{
					if (isAvailable(r,c))
						passed.Enqueue(squares[r, c]);
					else
						passed.Clear();

					if (passed.Count == lenght)
					{
						result.Add(passed.ToList());
					}
				}
			}

			return result;
		}

		private IEnumerable<Placement> GetAwailableVerticalPlacements(int lenght)
		{
			var result = new List<List<Square>>();

			for (int c = 0; c < Columns; c++)
			{
				LimitedQueue<Square> passed = new LimitedQueue<Square>(lenght);

				for (int r = 0; r < Rows; r++)
				{
					if (isAvailable(r,c))
						passed.Enqueue(squares[r, c]);
					else
						passed.Clear();

					if (passed.Count == lenght)
					{
						result.Add(passed.ToList());
					}
				}
			}
			return result;
		}
	}
}
