namespace Vsite.Oom.Battleship.Model;

public class Fleet
{
    private readonly List<Ship> ships = new();

    public IEnumerable<Ship> Ships => ships;

    public void CreateShip(IEnumerable<Square> squares)
    {
        var ship = new Ship(squares);
        ships.Add(ship);
    }
}