using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomSelector : ISequenceSqlector
    {
        private Random random = new Random();
        public IEnumerable<Square> Select(IEnumerable<IEnumerable<Square>> sequances)
        {
            return sequances.ElementAt(random.Next(0, sequances.Count()));
        }
    }
}
