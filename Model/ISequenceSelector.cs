namespace Vsite.Oom.Battleship.Model
{
    using Sequences = IEnumerable<IEnumerable<Square>>;
    using SquareSequence = IEnumerable<Square>;

    public interface ISequenceSelector
    {
        SquareSequence Select(Sequences sequences);
    }
}
