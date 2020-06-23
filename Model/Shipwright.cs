using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        private readonly int rows;
        private readonly int columns;
        private readonly ISquareTerminator terminator;

        public Shipwright(int rows, int columns, ISquareTerminator terminator)
        {
            this.rows = rows;
            this.columns = columns;
            this.terminator = terminator;
        }

        public Shipwright(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            terminator = new SquareTerminator(rows, columns);
        }

        public Shipwright()
        {
            rows = RulesSingleton.Instance.Rows;
            columns = RulesSingleton.Instance.Columns;
            terminator = new SquareTerminator(rows, columns);
        }

        public Fleet CreateFleet()
        {
            return CreateFleet(RulesSingleton.Instance.ShipLengths);
        }

        public Fleet CreateFleet(IEnumerable<int> shipLengths)
        {
            var random = new Random();
            for (int i = 0; i < 3; ++i)
            {
                var fleet = TryPlaceShips(shipLengths, random);
                if (fleet != null)
                {
                    return fleet;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        private Fleet TryPlaceShips(IEnumerable<int> shipLengths, Random random)
        {
            var lenghts = new List<int>(shipLengths.OrderByDescending(s => s));
            var grid = new Grid(rows, columns);
            var fleet = new Fleet();

            while (lenghts.Count > 0)
            {
                var placements = grid.GetAvailablePlacements(lenghts[0]);
                if (placements.Count() == 0) break;

                lenghts.RemoveAt(0);

                int index = random.Next(0, placements.Count());
                fleet.AddShip(placements.ElementAt(index));

                var toEliminate = terminator.ToEliminate(placements.ElementAt(index));
                grid.EliminateSquares(toEliminate);

                if (lenghts.Count() == 0) return fleet;
            }
            return fleet;
        }
    }
}
