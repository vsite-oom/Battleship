using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomSelector : ISequenceSelector
    {
        public IEnumerable<Square> Select(IEnumerable<IEnumerable<Square>> sequences)
        {
            var randIndex = random.Next(0, sequences.Count());
            return sequences.ElementAt(randIndex);
        }

        private readonly Random random = new();
    }
}
