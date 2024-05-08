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
		public Gunnery(int rows , int columns, IEnumerable<int> shipLengths)
		{
			recordGrid=new Grid(rows, columns);
		}
		public SquareCoordinate Next()
		{
			throw new NotImplementedException();
		}
		public void ProcessHitResult(hitResult hitResult)
		{
			throw new NotImplementedException();
		}
		public shootingTacticts shootingTacticts { get; private set; }=shootingTacticts.Random;

		private readonly Grid recordGrid;

		private ITargetSelector targetSelector=new RandomTargetSelector();
	}
}
