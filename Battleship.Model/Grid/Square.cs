namespace Battleship.Model;

/// <summary>Specifies the state of a <see cref="Square"/> on the grid.</summary>
public enum SquareState
{
    /// <summary>The square has not been targeted.</summary>
    Intact,
    /// <summary>The square has been eliminated from consideration as a ship placement.</summary>
    Eliminated,
    /// <summary>A shot fired at this square missed.</summary>
    Missed,
    /// <summary>A shot fired at this square hit a ship.</summary>
    Hit,
    /// <summary>The ship occupying this square has been sunk.</summary>
    Sunken
}

/// <summary>Represents a single cell on the game grid.</summary>
public class Square
{
    /// <summary>Initializes a new instance of the <see cref="Square"/> class.</summary>
    /// <param name="row">The row index of the square.</param>
    /// <param name="column">The column index of the square.</param>
    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        SquareState = SquareState.Intact;
    }

    /// <summary>Gets the row index of the square.</summary>
    public int Row { get; }

    /// <summary>Gets the column index of the square.</summary>
    public int Column { get; }

    /// <summary>Gets a value that indicates whether this square has been hit or sunk.</summary>
    /// <value><see langword="true"/> if the square state is <see cref="SquareState.Hit"/> or higher; otherwise, <see langword="false"/>.</value>
    public bool IsHit => SquareState >= SquareState.Hit;

    /// <summary>Gets the current state of this square.</summary>
    public SquareState SquareState { get; private set; }

    /// <summary>Marks the square as hit.</summary>
    public void Hit()
    {
        SquareState = SquareState.Hit;
    }

    /// <summary>Transitions the square to a new state only if the new state is higher than the current one.</summary>
    /// <param name="newState">One of the enumeration values that specifies the desired new state.</param>
    public void ChangeState(SquareState newState)
    {
        if (newState > SquareState)
        {
            SquareState = newState;
        }
    }
}