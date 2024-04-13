using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class FleetBuilder
    {
        private readonly Grid fleetGrid;
        private readonly List<int> shipLengths;
        public FleetBuilder(int gridRows,int gridColumns, int[] shipLength) {
            fleetGrid = new Grid(gridRows, gridColumns);
            shipLengths = new List<int>(shipLength);
        }
    }
}
