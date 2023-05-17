using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        private readonly RecordGrid grid;
        private readonly Square firstHit;
        private readonly IEnumerable<int> shipLengths;
        private readonly Random random = new();

        public ZoneShooting(RecordGrid grid, Square firstHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLengths = shipLengths;
        }
        public Square AddNextTarget()
        {
            List<IEnumerable<Square>> sequences = new();
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var seq = grid.GetAvailableSequence(firstHit, direction);
                if(seq.Count() > 0) {
                    sequences.Add(seq);
                }
            }
            var nextTargetSequence = sequences[random.Next(sequences.Count)];
            return nextTargetSequence.First();
        }
    }
}
