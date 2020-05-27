using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
		private Square lastTarget;
		public ShootingTactics ShootingTactics { get; private set; }
		private Grid evidenceGrid;
		private List<int> shipsToShoot;
		private Random random = new Random();
		private SortedSquares squaresHit = new SortedSquares();
		private ISquareTerminator squareTerminator;
		private ITargetSelect targetSelect;
		ShootingTacticFactory shootingTacticFactory;


		public Square NextTarget()
		{

			lastTarget = targetSelect.NextTarget();
			return lastTarget;
		}

		public Gunner(int rows, int columns, IEnumerable<int> shipLengths)
		{
			evidenceGrid = new Grid(rows, columns);
			shipsToShoot = new List<int>(shipLengths.OrderByDescending(l => l));
			ShootingTactics = ShootingTactics.Random;
			squareTerminator = new SquareTerminator(rows, columns);
			shootingTacticFactory = new ShootingTacticFactory(evidenceGrid, squaresHit, shipsToShoot);
			targetSelect = shootingTacticFactory.GetTactics(ShootingTactics.Random);
		}

		public void ProcessHitResult(HitResult hitResult)
		{

			evidenceGrid.MarkHitResult(lastTarget, hitResult);

			if (hitResult == HitResult.Missed)
				return;

			squaresHit.Add(lastTarget);

			if (hitResult == HitResult.Sunk)
			{
				var toEliminate = squareTerminator.ToEliminate(squaresHit);

				foreach (var sq in toEliminate)
					evidenceGrid.MarkHitResult(sq, HitResult.Missed);

				foreach (var sq in squaresHit)
					evidenceGrid.MarkHitResult(sq, HitResult.Sunk);


				int length = squaresHit.Length;
				shipsToShoot.Remove(length);
				squaresHit.Clear();
			}

			ChangeTactics(hitResult);



		}

		private void ChangeTactics(HitResult hitResult)
		{

			if (hitResult == HitResult.Sunk)
			{
				ShootingTactics = ShootingTactics.Random;
				
				return;
			}

			if (hitResult == HitResult.Hit)
			{
				switch (ShootingTactics)
				{
					case ShootingTactics.Random:
						ShootingTactics = ShootingTactics.Surrounding;
						break;

					case ShootingTactics.Surrounding:
						ShootingTactics = ShootingTactics.Inline;
						break;

					case ShootingTactics.Inline:
						return;

				}
			}

			targetSelect = shootingTacticFactory.GetTactics(ShootingTactics);
		}
	}
}
