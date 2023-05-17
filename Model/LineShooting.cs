using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(RecordGrid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            squares = squaresHit;
            this.shipLengths = shipLengths;
        }

        private readonly RecordGrid grid;
        private IEnumerable<Square> squares;
        private readonly IEnumerable<int> shipLengths;

        private readonly Random random = new Random();

        public Square NextTarget()
        {
            squares = squares.OrderBy(s => s.Row + s.Column);
            var sequences = new List<IEnumerable<Square>>();
            if (squares.First().Column == squares.Last().Column)
            {
                var s1 = grid.GetAvailableSequence(squares.First(), Direction.Upwards);
                if (s1.Any())
                {
                    sequences.Add(s1);
                }
                var s2 = grid.GetAvailableSequence(squares.Last(), Direction.Downwards);
                if (s2.Any())
                {
                    sequences.Add(s2);
                }
            }
            else
            {
                var s1 = grid.GetAvailableSequence(squares.First(), Direction.Leftwards);
                if (s1.Any())
                {
                    sequences.Add(s1);
                }
                var s2 = grid.GetAvailableSequence(squares.Last(), Direction.Rightwards);
                if (s2.Any())
                {
                    sequences.Add(s2);
                }
            }
            int index = random.Next(sequences.Count);
            return sequences[index].First();
        }
    }
}
