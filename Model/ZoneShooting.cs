namespace Vsite.Oom.Battleship.Model
{
    public class ZoneShooting : IShootingTactics
    {
        public ZoneShooting(RecordGrid grid, Square firstHit, IEnumerable<int> shipLengths)
        {
            this.grid = grid;
            this.firstHit = firstHit;
            this.shipLengths = shipLengths;
        }

        private readonly RecordGrid grid;
        private readonly IEnumerable<int> shipLengths;
        private readonly Square firstHit;
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
