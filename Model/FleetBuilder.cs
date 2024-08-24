using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(int gridRows, int gridColumns, int[] shipLengths)
        {
            this.gridRows = gridRows;
            this.gridColumns = gridColumns;
            this.shipLengths = new List<int>(shipLengths.OrderByDescending(length => length));  // Flotu stvaramo tako da prvo postavimo najveći brod, pa onda manje brodove. Zato sortiramo duljine brodova u silaznom redoslijedu.
        }

        private FleetGrid? fleetGrid;

        private readonly int gridRows, gridColumns;

        private readonly List<int> shipLengths; // "readonly ne znači da se njen sadržaj ne može mijenjati. Jedino ne možemo pozvati konstruktor s novom listom."

        public Fleet CreateFleet()
        {
            for (int i = 0; i < 100; ++i)  // Ako se ne uspije stvoriti flota u 100 pokušaja, vraća null.
            {
                var fleet = TryCreateFleet();
                if (fleet != null)
                    return fleet;
            }

            throw new InvalidOperationException("Unable to create fleet.");
        }

        private readonly Random random = new Random();  // "Da ne inicijalizira svaki puta taj generator slučajnih brojeva unutar metode staviti ćemo ga kao member klase."

        private readonly SquareEliminator eliminator = new SquareEliminator();

        private Fleet? TryCreateFleet()
        {
            var fleet = new Fleet();  // Nova flota bez brodova.
            fleetGrid = new FleetGrid(gridRows, gridColumns);  // Nova mreža bez eliminiranih polja.

            for (int i = 0; i < shipLengths.Count; ++i)
            {
                var candidates = fleetGrid.GetAvailablePlacements(shipLengths[i]);  // Kandidati su svi mogući položaji za brod duljine shipLengths[i]. | Ako nema slobodnih položaja za brod, GetAvailablePlacements vraća praznu kolekciju (kolekciju bez kandidata).
                if (!candidates.Any())
                    return null;

                var selectedIndex = random.Next(candidates.Count());  // Nasumično odabiremo index jednog od položaja-kandidata za brod. | Ako nema slobodnih položaja za brod, GetAvailablePlacements vraća praznu kolekciju, pa će candidates.Count() biti 0, a random.Next(0) će baciti iznimku.
                var selected = candidates.ElementAt(selectedIndex);  // Odabrani položaj.

                fleet.CreateShip(selected);

                var toEliminate = eliminator.ToEliminate(selected, fleetGrid.Rows, fleetGrid.Columns);  // Polja koja treba eliminirati jer su zauzeta.

                // Eliminiramo polja koja su zauzeta:
                foreach (var coordinate in toEliminate)
                {
                    fleetGrid.EliminateSquare(coordinate.Row, coordinate.Column);
                }
            }

            return fleet;
        }
    }
}
