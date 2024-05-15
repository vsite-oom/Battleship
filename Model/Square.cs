﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace vste.oom.battleship.model
{
	public enum SquareState
	{
		Intact,
		Eliminated,
		Missed,
		Hit,
		Sunken
	}
	public class Square
	{

		public Square(int row, int column)
		{
			Row = row;
			Column = column;
			squareState = SquareState.Intact;
		}
		public readonly int Row;
		public readonly int Column;

		public void Hit()
		{
			squareState = SquareState.Hit;
		}

		public void ChangeState(SquareState newState)
		{
			if ((int)newState > (int)squareState)
			{
				squareState = newState;
			}
		}

		public bool isHit => (int)squareState >= (int)SquareState.Hit;


		public SquareState squareState { get; private set; }
	}
}
