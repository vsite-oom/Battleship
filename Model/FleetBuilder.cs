using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var grid = new Grid(rules.GridRows, rules.GridColumns);
            var fleet = new Fleet();
            foreach (var shipLength in rules.ShipLengths)
            {
                var candidates = grid.GetAvailableSequences(shipLength);
                var selected = selector.Select(candidates);
                fleet.CreateShip(selected);
                var toEliminate = rules.Terminator.ToEliminate(selected);
                grid.RemoveSquares(toEliminate);
            }
            return fleet;
        }
    }
}