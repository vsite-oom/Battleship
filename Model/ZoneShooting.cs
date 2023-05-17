using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(RecordGrid grid, Square firstSquareHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            firstHit = firstSquareHit;
            this.shipLengths = shipLengths;
        }

        private readonly RecordGrid grid;
        private readonly Square firstHit;
        private readonly IEnumerable<int> shipLengths;

        private readonly Random random = new Random();

        public Square NextTarget()
        {
            var sequences = new List<IEnumerable<Square>>();
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var seq = grid.GetAvailableSequence(firstHit, direction);
                if (seq.Any())
                {
                    sequences.Add(seq);
                }
            }
            int index = random.Next(sequences.Count);
            return sequences[index].First();
        }
    }
}
