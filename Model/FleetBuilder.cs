namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(GameRules rules, ISequenceSelector selector)
        {
            this.rules = rules;
            this.selector = selector;
        }

        public FleetBuilder(GameRules rules) : this(rules, new RandomSelector())
        {
        }

        private readonly GameRules rules;
        private readonly ISequenceSelector selector;

        public Fleet CreateFleet()
        {
            var grid = new FleetGrid(rules.GridRows, rules.GridColumns);
            var fleet = new Fleet();
            foreach (var shipLength in rules.ShipLengths)
            {
                var candidates = grid.GetAvailableSequences(shipLength);
                var selected = selector.Select(candidates);
                fleet.CreateShip(selected);
                var ToEliminate = rules.Terminator.ToEliminate(selected);
                grid.RemoveSquares(ToEliminate);
            }
            return fleet;
        }
    }
}
