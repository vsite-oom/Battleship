namespace Model;

public abstract class Grid
{
    public int Rows { get; }
    public int Columns { get; }

    protected readonly Square?[,] _squares;

    protected Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _squares = new Square[rows, columns];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                _squares[r, c] = new Square(r, c);
            }
        }
    }

    public virtual IEnumerable<Square> Squares => _squares.Cast<Square>();

    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
    {
        return GetHorizontalPlacements(length)
            .Concat(GetVerticalPlacements(length));
    }

    protected abstract bool IsSquareAvailable(int row, int column);

    private IEnumerable<IEnumerable<Square>> GetHorizontalPlacements(int length)
    {
        var result = new List<IEnumerable<Square>>();

        for (int r = 0; r < Rows; r++)
        {
            var queue = new LimitedQueue<Square>(length);
            for (int c = 0; c < Columns; c++)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(_squares[r, c]!);
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

    private IEnumerable<IEnumerable<Square>> GetVerticalPlacements(int length)
    {
        var result = new List<IEnumerable<Square>>();

        for (int c = 0; c < Columns; c++)
        {
            var queue = new LimitedQueue<Square>(length);
            for (int r = 0; r < Rows; r++)
            {
                if (IsSquareAvailable(r, c))
                {
                    queue.Enqueue(_squares[r, c]!);
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
