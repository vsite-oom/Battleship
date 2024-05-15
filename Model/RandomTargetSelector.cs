namespace Vsite.Oom.Battleship.Model
{
    public class RandomTargetSelector : ITargetSelector
    {
        private readonly Grid grid;
        private readonly int shipLength;
        private readonly Random random = new ();

        public RandomTargetSelector(Grid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;
        }

        public Square Next()
        {
            var placements = grid.GetAvailablePlacements(shipLength);
            var candidates = placements.SelectMany(s => s);

            var selectedIndex = random.Next(candidates.Count());
            return candidates.ElementAt(selectedIndex);
        }
    }
}
