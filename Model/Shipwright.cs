using Model;
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
           rows= RulesSingleton.Instance.Rows ;
            columns=RulesSingleton.Instance.Columns;
        }
        public Shipwright()
        {
            this.rows = rows;
            this.columns = columns;
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

       Grid grid = new Grid(rows, columns); // 1. create grid    
        SquareTerminator terminator = new SquareTerminator(grid);
        Fleet fleet = new Fleet(); // 2. create fleet
            Random random = new Random();
            while (lengths.Count > 0)
        {
            var placements = grid.GetAvailablePlacements(lengths[0]); // 3. get available positions from grid for given length
            if (placements.Count() == 0)
                return null;
            lengths.RemoveAt(0);
            int index = random.Next(0, placements.Count()); // 4. select one position
            fleet.AddShip(placements.ElementAt(index)); // 5. forward position to fleet to create ship      
            var toEliminate = terminator.ToEliminate(placements.ElementAt(index)); // 6. eliminate squares from grid
            grid.EliminateSquares(toEliminate);
        }
                return fleet;
        }
        private readonly int rows;
        private readonly int columns;
   
    
        
      
    }
}