namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        public FleetBuilder(GameRules rules, ISequenceSelector selector)
        {
            this.rules = rules;
            this.selector = selector;
        }

        private readonly GameRules rules;
        private readonly ISequenceSelector selector;
        public Fleet CreateFleet()
        {
            var grid = new Grid(rules.GridRows, rules.GridColumns);
            var fleet = new Fleet();
            foreach (var shipLength in rules.ShipLengts)
            {
                var candidates = grid.GetAvailableSequences(shipLength);
                var Selected = selector.Select(candidates);
                fleet.CreateShip(Selected);
                var toEliminate = rules.Terminator.ToEliminate(Selected);
                grid.RemoveSquares(toEliminate);

            }
            return fleet;
        }
    }
}
