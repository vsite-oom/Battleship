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
            this.rows = rows;
            this.columns = columns;
            this.shipLengths = shipLengths;
        }

        public Shipwright(int rows, int columns, IEnumerable<int> shipLengths, ISquareEliminate eliminate)
        {
            this.rows = rows;
            this.columns = columns;
            this.shipLengths = shipLengths;
            eliminator = eliminate;
        }

        public Fleet CreateFleet()
        {
            int maxRetries = 5;
            for (int i = 0; i < maxRetries; ++i)
            {
                Fleet fleet = PlaceShips();
                if (fleet != null)
                {
                    fleet.RemainingShipNumber = fleet.Ships.Count();
                    return fleet;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        private Fleet PlaceShips()
        {
            Grid grid = new Grid(rows, columns);
            Fleet fleet = new Fleet();
            var sortedLengths = shipLengths.OrderByDescending(l => l);
            Queue<int> lengths = new Queue<int>(sortedLengths);
            while (lengths.Count > 0)
            {
                int len = lengths.Dequeue();
                var placements = grid.GetSequences(len);
                if (placements.Count() == 0)
                    return null;
                var index = random.Next(placements.Count());
                var selected = placements.ElementAt(index);
                fleet.CreateShip(selected);
                var toEliminate = eliminator.ToEliminate(selected);
                grid.RemoveSquares(toEliminate);
            }
            return fleet;
        }

        private readonly int rows;
        private readonly int columns;
        private readonly IEnumerable<int> shipLengths;
        private Random random = new Random();
        private ISquareEliminate eliminator = new SimpleSquareEliminator();
    }
}
