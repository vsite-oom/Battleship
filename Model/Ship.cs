namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public readonly IEnumerable<Square> Squares;

        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public bool Contains(int row, int column)
        {
            return Squares.FirstOrDefault(s => s.Row == row && s.Column == column) is not null;
        }
    }
}
