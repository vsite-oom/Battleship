using Battleship.Model;

namespace Battleship.Model;
/// <summary>Provides the base implementation for a rectangular grid of squares.</summary>
public abstract class Grid
{

    /// <summary>Initializes a new instance of the <see cref="Grid"/> class with the specified dimensions.</summary>
    /// <param name="rows">The number of rows in the grid.</param>
    /// <param name="columns">The number of columns in the grid.</param>
    protected Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        squares = new Square[Rows, Columns];

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                squares[r, c] = new Square(r, c);
            }
        }
    }

    /// <summary>Gets the number of rows in the grid.</summary>
    public int Rows { get; }

    /// <summary>Gets the number of columns in the grid.</summary>
    public int Columns { get; }

    /// <summary>The backing array of grid squares, indexed by [row, column].</summary>
    protected readonly Square?[,] squares;

    /// <summary>Gets all squares in the grid.</summary>
    public virtual IEnumerable<Square> Squares => squares.Cast<Square>();

    /// <summary>Returns all valid placements for a ship of the given length.</summary>
    /// <param name="length">The number of consecutive squares required.</param>
    /// <returns>An enumeration of candidate placements, each represented as a sequence of squares.</returns>

    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
    {
        return GetHorizontalAvailablePlacements(length).Concat(GetVerticalAvailablePlacements(length));
    }
    /// <summary>Determines whether the square at the given position is available.</summary>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    /// <returns><see langword="true"/> if the square is available; otherwise, <see langword="false"/>.</returns>
    protected abstract bool IsSquareAvailable(int row, int column);

    private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = [];

        for (int r = 0; r < Rows; ++r)
        {
            var queue = new LimitedQueue<Square>(length);
            for (int c = 0; c < Columns; ++c)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(squares[r, c]!);
                    if (queue.Count == length)
                    {
                        result.Add(queue.ToArray());
                    }
                }
                else
                {
                    queue.Clear();
                }
            }
        }
        return result;
    }

    private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = [];

        for (int c = 0; c < Columns; ++c)
        {
            var queue = new LimitedQueue<Square>(length);
            for (int r = 0; r < Rows; ++r)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(squares[r, c]!);
                    if (queue.Count == length)
                    {
                        result.Add(queue.ToArray());
                    }
                }
                else
                {
                    queue.Clear();
                }
            }
        }
        return result;
    }

}