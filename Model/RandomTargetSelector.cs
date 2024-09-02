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
            var placements = grid.GetAvailablePlacements(shipLength);
            var candidates = placements.SelectMany(s => s);

            // If there are no candidates available from the placements
            if (!candidates.Any())
            {
                // Fallback to selecting any intact squares from the grid
                candidates = grid.Squares.Where(s => s.SquareState == SquareState.Intact).ToList();
            }

            // Select a random index from the list of candidates
            var selectedIndex = random.Next(candidates.Count());

            return candidates.ElementAt(selectedIndex);
        }

        private readonly ShotsGrid grid;
        private readonly int shipLength;
        private readonly Random random = new Random();
    }
}