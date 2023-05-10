using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        private readonly Grid grid;
        private IEnumerable<Square> squaresHit;
        private readonly IEnumerable<int> shiplenghts;
        private readonly Random random=new Random();
        public LineShooting(Grid grid,IEnumerable<Square> squaresHit, IEnumerable<int> shipLenghts)
        {
            this.shiplenghts = shipLenghts;
            this.grid = grid;
            this.squaresHit=new List<Square>(squaresHit);
        }
        public Square NextTarget()
        {
            squaresHit = squaresHit.OrderBy(s=>s.row+s.column);
            var sequences = new List<IEnumerable<Square>>();
            if (squaresHit.First().column == squaresHit.Last().column)
            {
                var s1=grid.GetAvailableSequence(squaresHit.First(), Direction.Upwards);
                if(s1.Any())
                {
                    sequences.Add(s1);
                }
                var s2 = grid.GetAvailableSequence(squaresHit.Last(), Direction.Downwards);
                if (s2.Any())
                {
                    sequences.Add(s2);
                }
               
            }
            else
            {
                var s1 = grid.GetAvailableSequence(squaresHit.First(), Direction.Leftwards);
                if (s1.Any())
                {
                    sequences.Add(s1);
                }
                var s2 = grid.GetAvailableSequence(squaresHit.Last(), Direction.Rightwards);
                if (s2.Any())
                {
                    sequences.Add(s2);
                }
                
            }
            return sequences[random.Next(sequences.Count)].First();
        }
    }
}
