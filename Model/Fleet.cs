namespace Vsite.Oom.Battleship.Model
{
    public class Fleet
    {
        public void CreateShip(IEnumerable<Square> shipSquares)
        {
            ships.Add(new Ship(shipSquares));
        }

        public HitResult Fire(Square target)
        {
            foreach (var ship in ships)
            {
                HitResult result = ship.Fire(target);
                if (result != HitResult.Missed)
                {
                    return result;
                }
            }
            return HitResult.Missed;
        }

        private readonly List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships => ships;
    }

}
