using System;
using System.Collections.Generic;
using System.Linq;

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
            var placements = grid.GetAvailablePlacements(shipLength).ToList();
            var candidates = placements.SelectMany(s => s).ToList();

            if (!candidates.Any())
            {
                // Default to any remaining intact squares
                candidates = grid.Squares.Where(s => s.SquareState == SquareState.Intact).ToList();
                if (!candidates.Any())
                {
                    throw new InvalidOperationException("No valid targets available.");
                }
            }

            var selectedIndex = random.Next(candidates.Count);
            return candidates.ElementAt(selectedIndex);
        }

        private readonly ShotsGrid grid;
        private readonly int shipLength;
        private readonly Random random = new Random();
    }
}
