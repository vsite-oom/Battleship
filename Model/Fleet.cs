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

    public HitResult Hit(int row, int column)
    {
        HitResult hitResult = HitResult.Missed;
        foreach(var ship in ships)
        {
            hitResult = ship.Hit(row, column);
            if(hitResult == HitResult.Hit || hitResult == HitResult.Sunken)
            {
                return hitResult;
            }
        }

        return hitResult;
    }
}