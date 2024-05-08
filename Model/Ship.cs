﻿namespace vste.oom.battleship.model
{

	public enum hitResult
	{
		Missed,
		Hit,
		Sunken
	}
	public class Ship
	{
		public Ship(IEnumerable<Square> squares)
		{
			Squares=squares;
		}
		public readonly IEnumerable<Square> Squares;
		public bool Contains(int row, int column)
		{
			return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) !=null;
		}

		public hitResult Hit(int row, int column)
		{
			var square=Squares.FirstOrDefault(sq=>sq.Row == row && sq.Column == column);
			if (Contains(row, column) == false)
			{
				return hitResult.Missed;
			}
			square.Hit();

			if (Squares.All(sq => sq.isHit))
			{
				return hitResult.Sunken;
			}
			return hitResult.Hit;
		}

	}
}
