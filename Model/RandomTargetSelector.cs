using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomTargetSelector : ITargetSelector
    {
        public RandomTargetSelector(ShotsGrid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
        }
        public Square Next()
        {
            // Dohvaća sva moguća mjesta na kojima se brod duljine shipLength može postaviti.
            var placements = grid.GetAvailablePlacements(shipLength);

            if (!placements.Any())
            {
                throw new InvalidOperationException($"No available placements found for ship of length {shipLength}.");
            }

            // Spaja gornji niz nizova u jedan niz u kojemu se neka polja mogu pojaviti više puta.
            // To nam ne smeta jer ako se neko polje pojavljuje više puta, to znači da se na tom
            // mjestu brod može postaviti na više načina i onda je veća vjerojatnost da će se
            // to polje odabrati što i želimo jer je to polje s većom vjerojatnošću pogotka.
            // Još bolja taktika bi bila odabrati polje s najvećom vjerojatnošću pogotka, ali
            // to je već malo kompliciranije i zahtijevalo bi puno više vremena.
            var candidates = placements.SelectMany(s => s);

            // Odabire nasumično jedno od polja (veću vjerojatnost odabira imaju polja koja se pojavljuju više puta).
            var selectedIndex = random.Next(candidates.Count());

            // Vraća odabrano polje.
            return candidates.ElementAt(selectedIndex);
        }

        private readonly ShotsGrid grid;

        private readonly int shipLength;

        private readonly Random random = new Random();
    }
}
