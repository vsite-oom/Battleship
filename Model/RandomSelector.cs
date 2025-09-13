namespace Vsite.Oom.Battleship.Model
{
    public class RandomSelector : ISequenceSelector
    {
        public IEnumerable<Square> Select(IEnumerable<IEnumerable<Square>> sequences)
        {
            var sequenceList = sequences.ToList();
            
            if (sequenceList.Count == 0)
            {
                return Enumerable.Empty<Square>();
            }
            
            var index = random.Next(0, sequenceList.Count);
            return sequenceList[index];
        }

        private readonly Random random = new Random();
    }
}
