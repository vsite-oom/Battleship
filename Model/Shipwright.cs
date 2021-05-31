using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        public Shipwright(int rows, int columns, IEnumerable<int> shipLength)
        {
            this.rows = rows;
            this.columns = columns;
            shipLengths = shipLength.OrderByDescending(s => s);
        }

        public Fleet CreateFleet()
        {
            for (int retries = 0; retries < 5; ++retries)
            {
                var fleet = PlaceShips();
                if (fleet != null)
                    return fleet;
            }
            throw new ArgumentException();
        }

        private Fleet PlaceShips()
        {
            Queue<int> lengths = new Queue<int>(shipLengths);
            Grid grid = new Grid(rows, columns);
            Fleet fleet = new Fleet();
            SurroundingSquareEliminator eliminator = new SurroundingSquareEliminator(10, 10);
            IEnumerable<Square> ToRemove; 

            while (lengths.Count > 0)
            {
                int length = lengths.Dequeue();
                var placements = grid.GetAvailablePlacements(length);
                if (placements.Count() == 0)
                    return null;
                int index = random.Next(placements.Count());
                var selected = placements.ElementAt(index);
                fleet.CreateShip(selected);
                ToRemove = eliminator.ToEliminate(selected);
                grid.Eliminate(ToRemove);
            }
            return fleet;
        }

        private int rows;
        private int columns;
        private IEnumerable<int> shipLengths;
        private Random random = new Random();
    }
}
