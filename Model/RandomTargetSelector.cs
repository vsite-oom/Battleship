// Ignore Spelling: Vsite Oom
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
            //var placements = grid.GetAvailablePlacements(shipLength);
            //var candidates = placements.SelectMany(s => s);

            //var selectedIndex = random.Next(candidates.Count());
            //return candidates.ElementAt(selectedIndex);

            //TODO: Implement square target by count of how many
            var availablePlacements = grid.GetAvailablePlacements(shipLength);
            var availableSquares = availablePlacements.SelectMany(s => s);
            var grouped = availableSquares.GroupBy(x => x);
            var sorted = grouped.OrderByDescending(x => x.Count());
            var first = sorted.Select(grp => grp.Key).First();

            return first;
        }

        private readonly ShotsGrid grid;
        private readonly int shipLength;
        private readonly Random random = new Random();
    }
}
