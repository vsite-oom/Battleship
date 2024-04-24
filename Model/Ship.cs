using System.Collections.Immutable;

namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            Squares = squares;
        }

        public readonly IEnumerable<Square> Squares;

        public bool Contains(int row, int column)
        {
            return Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column) != null;
        }
    }

}
