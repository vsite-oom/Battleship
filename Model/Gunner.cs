using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
	public enum ShootingTactics
	{
		Random,
		Surrounding,
		Inline
	}
	public class Gunner
	{
		public Gunner(int rows, int cols, IEnumerable<int> shipLengths)
		{
			evidenceGrid = new Grid(rows, cols);
			shipToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
			ShootingTactics = ShootingTactics.Random;
			squareTerminator = new SquareTerminator(rows, cols);
			targetSelect = new RandomShooting(evidenceGrid);
		}
		public Square NextTarget()
		{
			lastTarget = targetSelect.NextTarget(shipToShoot[0]);
			return lastTarget;
		}

		public void ProcessHitResult(HitResult hitResult)
		{
			evidenceGrid.MarkHitResult(lastTarget, hitResult);
			if (hitResult == HitResult.Missed)
				return;
			squaresHit.Add(lastTarget);
			if(hitResult == HitResult.Sunken)
			{
				var toEliminate = squareTerminator.ToEliminate(squaresHit);
				foreach (var sq in toEliminate)
					evidenceGrid.MarkHitResult(sq, HitResult.Missed);
				foreach (var sq in squaresHit)
					evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
				int length = squaresHit.Length;
				shipToShoot.Remove(length);
				squaresHit.Clear();
			}
			ChangeTactics(hitResult);
		}

		private void ChangeTactics(HitResult hitResult)
		{
			if(hitResult == HitResult.Sunken)
			{
				ShootingTactics = ShootingTactics.Random;
				targetSelect = new RandomShooting(evidenceGrid);
				return;
			}
			if(hitResult == HitResult.Hit)
			{
				switch (ShootingTactics)
				{
					case ShootingTactics.Random:
						ShootingTactics = ShootingTactics.Surrounding;
						targetSelect = new SurroundingShooting(evidenceGrid, squaresHit);
						return;
					case ShootingTactics.Surrounding:
						ShootingTactics = ShootingTactics.Inline;
						targetSelect = new InlineShooting(evidenceGrid, squaresHit);
						return;
					case ShootingTactics.Inline:
						return;
				}
			}
		}


		private Square lastTarget;
		private Grid evidenceGrid;
		private List<int> shipToShoot;
		private Random random = new Random();
		private SortedSquares squaresHit = new SortedSquares();
		private ISquareTerminator squareTerminator;
		public ShootingTactics ShootingTactics { get; private set; }
		private ITargetSelect targetSelect;
	}
}
