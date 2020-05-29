using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
	using Placement = IEnumerable<Square>;

	public enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}
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

		public IEnumerable<Square> GetSquaresNextTo(Square square, Direction direction)
		{
			List<Square> result = new List<Square>();
			int row = square.Row;
			int column = square.Column;
			int deltaRow = 0;
			int deltaColumn = 0;
			int maxCount = 0;
			switch (direction)
			{
				case Direction.Right:
					++column;
					deltaColumn = +1;
					maxCount = Columns - column;
					break;
				case Direction.Down:
					++row;
					deltaRow = +1;
					maxCount = Rows - row;
					break;
				case Direction.Left:
					maxCount = column;
					--column;
					deltaColumn = -1;
					break;
				case Direction.Up:
					maxCount = row;
					--row;
					deltaRow = -1;
					break;
				default:
					Debug.Assert(false);
					break;
			}
			for (int i = 0; i < maxCount && isAvailable(row, column); ++i)
			{
				result.Add(squares[row, column]);
				row += deltaRow;
				column += deltaColumn;
			}
			return result;
		}

		public IEnumerable<IEnumerable<Square>> GetSquaresInLine(IEnumerable<Square> squaresHit)
		{
			List<Placement> result = new List<Placement>();

			if (squaresHit.First().Row == squaresHit.Last().Row)
			{

				var l = GetSquaresNextTo(squaresHit.First(), Direction.Left);
				if (l.Count() > 0)
					result.Add(l);

				l = GetSquaresNextTo(squaresHit.Last(), Direction.Right);
				if (l.Count() > 0)
					result.Add(l);

			}

			if (squaresHit.First().Column == squaresHit.Last().Column)
			{

				var l = GetSquaresNextTo(squaresHit.First(), Direction.Up);
				if (l.Count() > 0)
					result.Add(l);

				l = GetSquaresNextTo(squaresHit.Last(), Direction.Down);
				if (l.Count() > 0)
					result.Add(l);

			}
			else
				Debug.Assert(false);

			return result;
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
