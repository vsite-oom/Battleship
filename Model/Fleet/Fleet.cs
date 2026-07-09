namespace Model;

public class Fleet
{
    private readonly List<Ship> _ships = new();

    public IEnumerable<Ship> Ships => _ships;

    public void CreateShip(IEnumerable<Square> squares)
    {
        ArgumentNullException.ThrowIfNull(squares);
        _ships.Add(new Ship(squares));
    }

    public HitResult Hit(int row, int column)
    {
        foreach (var ship in _ships)
        {
            var result = ship.Hit(row, column);
            if (result != HitResult.Missed)
                return result;
        }
        return HitResult.Missed;
    }
}
