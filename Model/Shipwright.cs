using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Shipwright
    {
        public Shipwright(int rows, int columns, ISquareTerminator terminator) {
            this.rows = rows;
            this.columns = columns;
            this.terminator = terminator;
        }

        public Shipwright(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.terminator = new SquareTerminator(rows,columns);
        }

        public Shipwright()
        {
            rows = RulesSingleton.Instance.Rows;
            columns = RulesSingleton.Instance.Columns;
        }

        public Fleet CreateFleet(IEnumerable<int> shipLenght) {

           for(int i = 0; i<3;++i)
            {
                Fleet fleet = PlaceShips(shipLenght);
                if (fleet != null)
                    return fleet;
              
            }
           
            
            throw new ArgumentOutOfRangeException();
        }

        private Fleet PlaceShips(IEnumerable<int> shipLenght) {
            
            List<int> lenghts = new List<int>(shipLenght.OrderByDescending(x => x));

            Grid grid = new Grid(rows, columns);           
            Fleet fleet = new Fleet();
 
            while (lenghts.Count > 0)
            {
                var placments = grid.GetAvailablePlacements(lenghts[0]);
                if (placments.Count() == 0)
                    return null;
                    
                lenghts.RemoveAt(0);

                int index = random.Next(0, placments.Count());

                fleet.AddShip(placments.ElementAt(index));
                // 6. eliminate squares form grid
                var toEliminate = terminator.ToEliminate(placments.ElementAt(index));
                grid.EliminateSquares(toEliminate);
                
            }
            return fleet;
        }
        private Random random = new Random();
        private readonly int rows;
        private readonly int columns;
        private readonly ISquareTerminator terminator;
   
       
    
    }
}
