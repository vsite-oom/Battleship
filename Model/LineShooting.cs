using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        private readonly RecordGrid grid;
        private List<Square> squares;
        private readonly IEnumerable<int> shipLengths;
        public LineShooting(RecordGrid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            squares = new List<Square>(squaresHit);
            this.shipLengths = shipLengths;
        }
            public Square AddNextTarget()
        {
            squares = squares.OrderBy(x => x.Row + x.Column).ToList();
            var sequences = new List<IEnumerable<Square>>();
            
            if(squares.First().Column == squares.Last().Column)
            {
                var s1 = grid.GetAvailableSequence(squares.First(), Direction.Upwards);
                if (s1.Any()) sequences.Add(s1);
            
                var s2 = grid.GetAvailableSequence(squares.Last(), Direction.Downwards);
                if (s2.Any()) sequences.Add(s2);
            }
            else
            {
                var s1 = grid.GetAvailableSequence(squares.First(), Direction.Leftwards);
                if (s1.Any()) sequences.Add(s1);

                var s2 = grid.GetAvailableSequence(squares.Last(), Direction.Rightwards);
                if (s2.Any()) sequences.Add(s2);
            }
            return sequences[new Random().Next(sequences.Count())].First();
        }
    }
}
