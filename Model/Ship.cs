namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        private readonly IEnumerable<Square> squares;

        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares;
        }

        public bool Contains(int row, int column)
        {
            return squares.FirstOrDefault(s => s.Row == row && s.Column == column) is not null;
        }
    }
}
