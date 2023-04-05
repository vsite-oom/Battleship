using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsie.Oom.Battleship.Model
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

        public IEnumerable<Square> ToEliminat { get; private set; }

        public Fleet CreateFleet()
        {
            var grid = new Grid(rules.GridRows, rules.GridColumns);
            var fleet =new Fleet();
            foreach(var shipLength in rules.ShipLengths){
                var candidates = grid.GetAvailableSequences(shipLength);
                var selected = selector.Select(candidates);
                fleet.CreateShip(selected);
                rules.Terminator.ToEliminat(selected);
                grid.RemoveSquareSequence(ToEliminat);
            }
            return fleet;

        }
    }
}
