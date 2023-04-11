using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetBuilder
    {
        private readonly GameRules rules;
        private readonly ISequenceSelector selector;

        public FleetBuilder(GameRules rules, ISequenceSelector selector)
        {
            this.rules = rules;
            this.selector = selector;

        }

        public Fleet CreateFleet()
        {
            var grid = new Grid(rules.gridRows, rules.gridColumns);
            var fleet = new Fleet();
            foreach (var shipLenght in rules.shipLenghts)
            {
                var candidates = grid.GetAvailableSequences(shipLenght);
                var selected = selector.Select(candidates);

                fleet.createShip(selected);
                var toEliminate = rules.terminator.ToEliminate(selected);
                grid.RemoveSquares(toEliminate);
            }

            return fleet;
        }
    }
}
