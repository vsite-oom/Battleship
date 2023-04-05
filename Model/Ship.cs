namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public Ship(IEnumerable<Square> sqaures)
        {
            Squares = sqaures;

        }
        public readonly IEnumerable<Square> Sqaures;

        public IEnumerable<Square> Squares { get; }
    }
}
