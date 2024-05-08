

namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships { get { return ships; } }

        public void CreateShip(IEnumerable<Square> squares)
        {
            ships.Add(new Ship(squares));
        }

        public HitResult Hit(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
