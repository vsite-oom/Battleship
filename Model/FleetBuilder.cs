using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shipLengths = new List<int>(shipLengths);
        }


        private readonly Grid fleetGrid;

        private readonly List<int> shipLengths; // "readonly ne znači da se njen sadržaj ne može mijenjati. Jedino ne možemo pozvati konstruktor s novom listom."
    }
}
