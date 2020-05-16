using System;
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
		}
		public Square NextTarget()
		{
			lastTarget = SelectTarget();
			return lastTarget;
		}

		public void ProcessHitResult(HitResult hitResult)
		{
			evidenceGrid.MarkHitResult(lastTarget, hitResult);
			switch (hitResult)
			{
				case HitResult.Missed:
					return;
				case HitResult.Sunken:
					squaresHit.Add(lastTarget);
					squaresHit = squaresHit.OrderBy(s => s.Row + s.Column).ToList();
					var toEliminate = squareTerminator.ToEliminate(squaresHit);
					foreach (var sq in toEliminate)
						evidenceGrid.MarkHitResult(sq, HitResult.Missed);
					foreach (var sq in squaresHit)
						evidenceGrid.MarkHitResult(sq, HitResult.Sunken);
					int length = squaresHit.Count();
					shipToShoot.Remove(length);
					squaresHit.Clear();
					ShootingTactics = ShootingTactics.Random;
					return;
				case HitResult.Hit:
					squaresHit.Add(lastTarget);
					squaresHit = squaresHit.OrderBy(s => s.Row + s.Column).ToList();
					switch (ShootingTactics)
					{
						case ShootingTactics.Random:
							ShootingTactics = ShootingTactics.Surrounding;
							return;
						case ShootingTactics.Surrounding:
							ShootingTactics = ShootingTactics.Inline;
							return;
						case ShootingTactics.Inline:
							return;
					}
					break;
			}
		}
		private Square SelectTarget()
		{
			switch (ShootingTactics)
			{
				case ShootingTactics.Random:
					return SelectRandomly();
				case ShootingTactics.Surrounding:
					return SelectFromArround();
				case ShootingTactics.Inline:
					return SelectInline();
				default:
					Debug.Assert(false);
					return null;
			}
		}
		private Square SelectRandomly()
		{
			var placements = evidenceGrid.GetAvailablePlacements(shipToShoot[0]);
			var allCandidates = placements.SelectMany(seq => seq);
			int index = random.Next(0, allCandidates.Count());
			return allCandidates.ElementAt(index);
		}

		private Square SelectFromArround()
		{
			List<IEnumerable<Square>> arround = new List<IEnumerable<Square>>();
			foreach (Direction direction in Enum.GetValues(typeof(Direction)))
			{
				var l = evidenceGrid.GetSquaresNextTo(lastTarget, direction);
				if (l.Count() > 0)
					arround.Add(l);
			}
			if (arround.Count == 1)
				return arround[0].First();
			//TODO: improve selection so that only largest lists are taken as candidates 
			int index = random.Next(0, arround.Count);
			return arround[index].First();
		}

		private Square SelectInline()
		{
			var l = evidenceGrid.GetSquaresInline(squaresHit);
			if (l.Count() == 1)
				return l.ElementAt(0).First();
			//TODO: improve selection only largest lists are taken as candidates
			//l.OrderByDescending(ls => l.Count());//sortirati po duljini, najdulja na dole i izabrati najdulje
			int index = random.Next(0, l.Count());
			return l.ElementAt(index).First();

		}

		private Square lastTarget;
		private Grid evidenceGrid;
		private List<int> shipToShoot;
		private Random random = new Random();
		private List<Square> squaresHit = new List<Square>();
		private ISquareTerminator squareTerminator;
		public ShootingTactics ShootingTactics { get; private set; }
	}
}