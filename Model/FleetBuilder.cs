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
            Fleet fleet;
            
            // Only a few iterations in worst case scenario, trust me
            while (true)
            {
                fleet = TryCreateFleet();
                if (fleet != null)
                    break;
            }
            
            return fleet;
        }

        private Fleet TryCreateFleet()
        {
            var grid = new FleetGrid(rules.GridRows, rules.GridColumns);
            var fleet = new Fleet();
            
            foreach (var shipLength in rules.ShipLengths)
            {
                var candidates = grid.GetAvailableSequences(shipLength);
                var selected = selector.Select(candidates);
                
                if (!selected.Any())
                {
                    return null;
                }
                
                fleet.CreateShip(selected);
                var toEliminate = rules.Terminator.ToEliminate(selected);
                grid.RemoveSquares(toEliminate);
            }
            
            return fleet;
        }
    }
}
