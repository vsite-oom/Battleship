using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        private readonly Grid grid;
        private readonly Square firstHit;
        private readonly IEnumerable<int> shiplenghts;
        private readonly Random random=new Random();
        public ZoneShooting(Grid grid,Square firstHit, IEnumerable<int> shipLenghts)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shiplenghts = shipLenghts;
        }
        public Square NextTarget()
        {
            var sequences = new List<IEnumerable<Square>>();
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var seq =grid.GetAvailableSequence(firstHit, direction);
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
