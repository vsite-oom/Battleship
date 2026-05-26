using Battleship.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Model
{

    /// <summary>Builds a <see cref="Fleet"/> by randomly placing ships on a <see cref="FleetGrid"/>.</summary>
    public class FleetBuilder
    {

        /// <summary>Initializes a new instance of the <see cref="FleetBuilder"/> class.</summary>
        /// <param name="gridRows">The number of rows in the placement grid.</param>
        /// <param name="gridColumns">The number of columns in the placement grid.</param>
        /// <param name="shipLengths">The lengths of the ships to place, in any order.</param>
        /// <exception cref="ArgumentNullException"><paramref name="shipLengths"/> is <see langword="null"/>.</exception>
        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
        {
            ArgumentNullException.ThrowIfNull(shipLengths);
            fleetGrid = new FleetGrid(gridRows, gridColumns);
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));
        }

        private readonly FleetGrid fleetGrid;

        private readonly List<int> shipLengths;

        private readonly Random random = new Random();

        private readonly SquareEliminator eliminator = new SquareEliminator();

        /// <summary>Creates a fleet with all ships placed randomly on the grid.</summary>
        /// <returns>A new <see cref="Fleet"/> with all ships placed.</returns>
        public Fleet CreateFleet()
        {
            var fleet = new Fleet();

            for (int i = 0; i < shipLengths.Count; ++i)
            {
                var candidates = fleetGrid.GetAvailablePlacements(shipLengths[i]).ToArray();
                var selectedIndex = random.Next(candidates.Length);
                var selected = candidates[selectedIndex];

                fleet.CreateShip(selected);
                var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);
                foreach (var coordinate in toEliminate)
                {
                    fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                }
            }

            return fleet;
        }
    }
}
