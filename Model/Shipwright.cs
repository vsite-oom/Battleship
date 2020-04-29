using System;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        public Shipwright(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public Shipwright()
        {
            rows = RulesSingleton.Instance.Rows;
            columns = RulesSingleton.Instance.Columns;
        }

        public Fleet CreateFleet(IEnumerable<int> shipLengths)
        {
            for( int i = 0; i < 3; ++i)
            {
                var fleet = TryPlaceShips(shipLengths);
                if (fleet != null)
                {
                    return fleet;
                }
            }
            throw new ArgumentOutOfRangeException(); ;
        }

        private Fleet TryPlaceShips(IEnumerable<int> shipLengths)
        {
            var lenghts = new List<int>(shipLengths.OrderByDescending(s => s));
            var grid = new Grid(rows, columns);
            var terminator = new SquareTerminator(grid);
            var fleet = new Fleet();
            var random = new Random();

            while (lenghts.Count > 0)
            {
                var placements = grid.GetAvailablePlacements(lenghts[0]);
                if (placements.Count() == 0) break;

                lenghts.RemoveAt(0);

                int index = random.Next(0, placements.Count());
                fleet.AddShip(placements.ElementAt(index));

                var toEliminate = terminator.ToEliminate(placements.ElementAt(index));
                grid.EliminateSqure(toEliminate);

                if (lenghts.Count() == 0) return fleet;
            }
            return fleet;
        }

        private readonly int rows;
        private readonly int columns;
    }
}
