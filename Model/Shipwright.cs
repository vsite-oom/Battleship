using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            for (int i = 0; i < 3; ++i)
            {
                Fleet fleet = PlaceShips(shipLengths);
                if (fleet != null)
                    return fleet;
            }
            throw new ArgumentOutOfRangeException();

        }

        private Fleet PlaceShips(IEnumerable<int> shipLengths)
        {
            List<int> lengths = new List<int>(shipLengths.OrderByDescending(x => x));
            // 1. create grid
            Grid grid = new Grid(rows, columns);
            SquareTerminator terminator = new SquareTerminator(grid);
            // 2. create fleet
            Fleet fleet = new Fleet();
            Random random = new Random();
            while (lengths.Count > 0)
            {
                // 3. get available positions from grid for given length
                var placements = grid.GetAvailablePlacements(lengths[0]);
                if (placements.Count() == 0)
                    return null;
                lengths.RemoveAt(0);
                // 4. select one position
                int index = random.Next(0, placements.Count());
                // 5. forward position to fleet to create ship
                fleet.AddShip(placements.ElementAt(index));
                // 6. eliminate squares from grid
                var toEliminate = terminator.ToEliminate(placements.ElementAt(index));
                grid.EliminateSquares(toEliminate);
            }
            return fleet;
        }

        private readonly int rows;
        private readonly int columns;
    }
}