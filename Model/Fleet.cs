namespace Vsite.Oom.Battleship.Model

{
    public class Fleet
    {
        private List<Ship> ships = new List<Ship>();

        public IEnumerable<Ship> Ships { get { return ships; } }

        public void CreateShip(IEnumerable<Square>squares)
        {
            var ship = new Ship(squares);
            ships.Add(ship);
        }

        public HitResult Hit(int row, int column)
        {
            foreach (var ship in ships)
            {
                foreach (var square in ship.Squares)
                {
                    if (square.Row == row && square.Column == column)
                    {
                        if (square.IsHit)
                            continue;

                        square.Hit();

                        if (ship.Squares.All(s => s.IsHit))
                            return HitResult.Sunken; 

                        return HitResult.Hit;
                    }
                }
            }

            return HitResult.Missed;
        }
    }
}
