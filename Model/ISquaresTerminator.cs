namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    public interface ISquaresTerminator
    {
        SquareSequence ToEliminate(SquareSequence shipSquares);
    }
}
