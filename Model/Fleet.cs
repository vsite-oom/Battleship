namespace Vsite.Oom.Battleship.Model

{
    public class Fleet
    {
        public void CreateShip(IEnumerable<Square> shipSquares)
        {
            ships.Add(new Ship(shipSquares));
        }

        private readonly List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships => ships;
    }

}
