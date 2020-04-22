using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class Shipwright
    {
        public Shipwright(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public Fleet CreateFleet(IEnumerable<int> shipLengths)
        {
            
            for(int i = 0; i < 3; ++i)
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
            Grid grid = new Grid(rows, columns);
            SquareTerminator terminator = new SquareTerminator(grid);
            Fleet fleet = new Fleet();
            Random random = new Random();

            while (lengths.Count > 0)
            {
                var placements = grid.GetAvailablePlacements(lengths[0]);
                if (placements.Count() == 0)
                {
                    return null;
                }
                lengths.RemoveAt(0);
                var index = random.Next(0, placements.Count());
                fleet.AddShip(placements.ElementAt(index));

                var toEliminate = terminator.toEliminate(placements.ElementAt(index));
                grid.EliminateSquares(toEliminate);

               
            }
            return fleet;
        }

        private readonly int rows;
        private readonly int columns;
    }
}
