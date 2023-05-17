namespace Vsite.Oom.Battleship.Model
{
    public class LineShooting : IShootingTactics
    {
        public LineShooting(RecordGrid grid, IEnumerable<Square> squaresHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.shipLengths = shipLengths;
            this.squaresHit = squaresHit;
        }

        private readonly RecordGrid grid;
        private readonly IEnumerable<int> shipLengths;
        private IEnumerable<Square> squaresHit;
        private readonly Random random = new Random();

        public Square NextTarget()
        {
            squaresHit = squaresHit.OrderBy(s => s.Row + s.Column);
            var sequences = new List<IEnumerable<Square>>();
            if (squaresHit.First().Column == squaresHit.Last().Column)
            {
                var s1 = grid.GetAvailableSequence(squaresHit.First(), Direction.Upwards);
                if (s1.Any())
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
            int index = random.Next(sequences.Count);
            return sequences[index].First();

        }
    }
}
