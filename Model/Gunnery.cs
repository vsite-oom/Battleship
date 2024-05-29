using System;
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
			recordGrid = new ShotsGrid(rows, columns);
			this.shipLengths = new List<int>(shipLengths.OrderDescending());
			targetSelector = new RandomTargetSelector(recordGrid, this.shipLengths[0]);
		}
		public Square Next()
		{
			target = targetSelector.Next();
			return target;
		}
		public void ProcessHitResult(hitResult hitResult)
		{
			RecordTargetResult(hitResult);
			if (hitResult == hitResult.Hit)
			{
				switch (shootingTacticts)
				{
					case shootingTacticts.Random:
						shootingTacticts = shootingTacticts.Surrounding;
						targetSelector = new SurroundingTargetSelector(recordGrid,target, shipLengths[0]);
						break;
					case shootingTacticts.Surrounding:
						shootingTacticts = shootingTacticts.Inline;
						targetSelector = new InlineTargetSelector(recordGrid, shipSquares, shipLengths[0]);
						break;
				}
			}
			else if (hitResult == hitResult.Sunken)
			{
				shootingTacticts = shootingTacticts.Random;
				targetSelector = new RandomTargetSelector(recordGrid, shipLengths[0]);
			}
		}

		private void RecordTargetResult(hitResult hitResult)
		{
			switch (hitResult)
			{
				case hitResult.Missed:
					target.ChangeState(SquareState.Missed);
					return;
				case hitResult.Hit:
					target.ChangeState(SquareState.Hit);
					shipSquares.Add(target);
					return;
				case hitResult.Sunken:
					MarkShipSunken();
					return;


			}
		}

		private void MarkShipSunken()
		{
			shipSquares.Add(target);
			foreach (var square in shipSquares)
			{
				square.ChangeState(SquareState.Sunken);
			}
			var ToEliminate = eliminator.ToEliminate(shipSquares, recordGrid.Rows, recordGrid.Columns);
			foreach(var square in shipSquares)
			{
				recordGrid.ChangeSquareState(square.Row, square.Column, SquareState.Eliminated);
			}
			shipSquares.Clear();
		}

		public shootingTacticts shootingTacticts { get; private set; } = shootingTacticts.Random;

		private readonly ShotsGrid recordGrid;

		private readonly List<int> shipLengths = [];

		private List<Square> shipSquares = new List<Square>();

		private Square target;

		private ITargetSelector targetSelector;

		private readonly SquareEliminator eliminator = new SquareEliminator();
	}
}
