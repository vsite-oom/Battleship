﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public enum shootingTacticts
	{
		Random,
		Surrounding,
		Inline
	}
	public class Gunnery
	{

		public Gunnery(int rows, int columns, IEnumerable<int> shipLengths)
		{
			recordGrid = new Grid(rows, columns);
		}
		public Square Next()
		{
			target = targetSelector.Next();
			return target;
		}
		public void ProcessHitResult(hitResult hitResult)
		{
			if (hitResult == hitResult.Hit)
			{
				switch (shootingTacticts)
				{
					case shootingTacticts.Random:
						shootingTacticts = shootingTacticts.Surrounding;
						targetSelector = new SurroundingTargetSelector();
						break;
					case shootingTacticts.Surrounding:
						shootingTacticts = shootingTacticts.Inline;
						targetSelector = new InlineTargetSelector();
						break;
				}
			}
			else if (hitResult == hitResult.Sunken)
			{
				shootingTacticts = shootingTacticts.Random;
				targetSelector = new RandomTargetSelector();
			}
		}
		public shootingTacticts shootingTacticts { get; private set; } = shootingTacticts.Random;



		private readonly Grid recordGrid;
		private Square target;

		private ITargetSelector targetSelector = new RandomTargetSelector();
	}
}
