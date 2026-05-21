namespace Battleship.Model;

/// <summary>Represents the collection of ships belonging to one player.</summary>
public class Fleet
{
    private readonly List<Ship> ships = [];

    /// <summary>Gets the ships in the fleet.</summary>
    public IEnumerable<Ship> Ships => ships;

    /// <summary>Creates a new ship from the given squares and adds it to the fleet.</summary>
    /// <param name="squares">The squares occupied by the new ship.</param>
    /// <exception cref="ArgumentNullException"><paramref name="squares"/> is <see langword="null"/>.</exception>
    public void CreateShip(IEnumerable<Square> squares)
    {
        ArgumentNullException.ThrowIfNull(squares);
        var ship = new Ship(squares);
        ships.Add(ship);
    }

    /// <summary>Fires a shot at the given position and returns the outcome across all ships.</summary>
    /// <param name="row">The row index of the shot.</param>
    /// <param name="column">The column index of the shot.</param>
    /// <returns>
    /// The <see cref="HitResult"/> from the first ship that occupies the position,
    /// or <see cref="HitResult.Missed"/> if no ship occupies that position.
    /// </returns>
    public HitResult Hit(int row, int column)
    {
        foreach (var ship in ships)
        {
            var result = ship.Hit(row, column);
            if (result != HitResult.Missed)
            {
                return result;
            }
        }
        return HitResult.Missed;
    }
}
