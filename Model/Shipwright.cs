using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        private readonly int rows;
        private readonly int columns;
        private readonly IEnumerable<int> shipLengths;
        private readonly Random random = new Random();

        public Shipwright(int rows, int columns, IEnumerable<int> shipLengths)
        {
            this.rows = rows;
            this.columns = columns;
            this.shipLengths = shipLengths.OrderByDescending(s => s);
        }

        public Fleet CreateFleet(List<int> shipLengths)
        {
            for (int retries = 0; retries < 5; ++retries)
            {
                var fleet = PlaceShips();

                if (fleet != null)
                {
                    return fleet;
                }
            }
            throw new ArgumentException();
        }

        private Fleet PlaceShips()
        {
            Queue<int> lengths = new Queue<int>(shipLengths);
            Grid grid = new Grid(rows, columns, new SurroundingSquaresEliminator(rows, columns));
            Fleet fleet = new Fleet();

            while (lengths.Count > 0)
            {
                int length = lengths.Dequeue();
                var placements = grid.GetAvailablePlacements(length);

                if (placements.Count() == 0)
                {
                    return null;
                }

                int index = random.Next(placements.Count());
                var selected = placements.ElementAt(index);

                fleet.CreateShip(selected);
                grid.Eliminate(selected);
            }
            return fleet;
        }
    }
}