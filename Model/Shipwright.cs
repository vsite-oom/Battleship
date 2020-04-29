using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {

        private readonly int rows;
        private readonly int columns;
        Random random = new Random();
        public Shipwright(int rows, int columns)
        {
            this.columns = columns;
            this.rows = rows;
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
                Fleet fleet = TryPlaceShips(shipLengths);

                if (fleet != null)
                    return fleet;
            }
            throw new ArgumentOutOfRangeException();
        }

        private Fleet TryPlaceShips(IEnumerable<int> shipLengths)
        {
            List<int> lengths = new List<int>(shipLengths.OrderByDescending(l => l));
            Grid grid = new Grid(rows, columns);            
            SquareTerminator terminator = new SquareTerminator(rows, columns);
            Fleet fleet = new Fleet();

            while (lengths.Count > 0)
            {

                var placements = grid.GetAvailablePlacements(lengths[0]);

                if (placements.Count() == 0)
                    return null;

                lengths.RemoveAt(0);
                int index = random.Next(0, placements.Count());
                fleet.AddShip(placements.ElementAt(index));

                var toEliminate = terminator.ToEliminate(placements.ElementAt(index));
                grid.EliminateSquares(toEliminate);                               
            }
            return fleet;
        }
    }
}
