using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vste.oom.battleship.model
{
	public class FleetBuilder
	{
		public FleetBuilder(int gridRows, int gridColums, int[] shipLengths) 
		{
			fleetGrid = new Grid(gridRows, gridColums);
			this.shipLengths=new List<int>(shipLengths);
		}

		private readonly Grid fleetGrid;
		private readonly List<int> shipLengths;
	}
}
