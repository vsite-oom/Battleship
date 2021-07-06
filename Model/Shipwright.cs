using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        public Shipwright(int rows, int columns, IEnumerable<int> shipLengths)
        {
            this.rows        = rows;
            this.columns     = columns;
            this.shipLengths = shipLengths.OrderByDescending(s => s);
        }

        public Fleet CreateFleet()
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
            Grid grid = new Grid(rows, columns, new SurroundingSquareEliminator(rows, columns));
            Fleet fleet = new Fleet();

            while (lengths.Count > 0)
            {
                int shipLength = lengths.Dequeue();
                var shipPlacements = grid.GetAvailablePlacements(shipLength);

                if (shipPlacements.Count() == 0)
                {
                    return null;
                }

                int index = random.Next(shipPlacements.Count());
                var selected = shipPlacements.ElementAt(index);

                grid.Eliminate(selected);
                fleet.CreateShip(selected);
            }
            return fleet;
        }

        private int rows;
        private int columns;
        private IEnumerable<int> shipLengths;
        private Random random = new Random();
    }
}
