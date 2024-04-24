using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shiplengths)
        {
            fleetGrid = new Grid(gridRows, gridColumns);
            this.shiplengths = new List<int>(shiplengths.OrderByDescending(length => length));
        }

        private readonly Grid fleetGrid;

        private readonly List<int> shiplengths;

        public Fleet CreateFleet()
        {
            var fleet = new Fleet();
            //doma dodati petlju ako je null onda pozvati ponovo jer  više nema mjesta za brodove, tako da generira cijelu ploču ponovo
            for (int i = 0; i < shiplengths.Count; i++)
            {
                var candidates = fleetGrid.GetVerticalAvailablePlacements(shiplengths[i]);
                var selectedIndex = random.Next(candidates.Count());
                var selected = candidates.ElementAt(selectedIndex);

                fleet.CreateShip(selected);

                var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);
                foreach(var coordinate in toEliminate)
                {
                    fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                }

            }

            return fleet;
        }

        private readonly Random random = new Random();

        private readonly SquareEliminator eliminator = new SquareEliminator();
       
    }
}
