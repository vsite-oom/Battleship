namespace Battleship.Model;

/// <summary>Specifies the result of a hit attempt on a ship.</summary>
public enum HitResult
{
    /// <summary>The shot did not hit any square of the ship.</summary>
    Missed,
    /// <summary>The shot hit a square of the ship, but the ship is not yet sunk.</summary>
    Hit,
    /// <summary>The shot sank the ship.</summary>
    Sunken
}

/// <summary>Represents a battleship occupying one or more squares on the fleet grid.</summary>
public class Ship
{
    /// <summary>Initializes a new instance of the <see cref="Ship"/> class.</summary>
    /// <param name="squares">The squares occupied by this ship.</param>
    /// <exception cref="ArgumentNullException"><paramref name="squares"/> is <see langword="null"/>.</exception>
    public Ship(IEnumerable<Square> squares)
    {
        ArgumentNullException.ThrowIfNull(squares);
        Squares = squares.ToArray();
    }

    /// <summary>Gets the squares occupied by this ship.</summary>
    public IReadOnlyList<Square> Squares { get; }

    /// <summary>Determines whether this ship occupies a given grid position.</summary>
    /// <param name="row">The row index to check.</param>
    /// <param name="column">The column index to check.</param>
    /// <returns><see langword="true"/> if the ship occupies the specified position; otherwise, <see langword="false"/>.</returns>
    public bool Contains(int row, int column)
    {
        return Squares.Any(sq => sq.Row == row && sq.Column == column);
    }

    /// <summary>Applies a shot at the given position and returns the outcome.</summary>
    /// <param name="row">The row index of the shot.</param>
    /// <param name="column">The column index of the shot.</param>
    /// <returns>
    /// <see cref="HitResult.Missed"/> if the position is not part of this ship;
    /// <see cref="HitResult.Hit"/> if the ship was hit but not yet sunk;
    /// <see cref="HitResult.Sunken"/> if this shot sank the ship.
    /// </returns>
    public HitResult Hit(int row, int column)
    {
        var square = Squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column);
        if (square == null)
        {
            return HitResult.Missed;
        }

        square.Hit();

        if (Squares.All(sq => sq.IsHit))
        {
            foreach (var sq in Squares)
            {
                sq.ChangeState(SquareState.Sunken);
            }

            return HitResult.Sunken;
        }

        return HitResult.Hit;
    }
}
