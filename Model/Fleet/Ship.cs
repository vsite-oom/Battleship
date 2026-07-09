namespace Model;

public class Ship
{
    private readonly Square[] _squares;
    private readonly bool[] _hits;

    public Ship(IEnumerable<Square> squares)
    {
        ArgumentNullException.ThrowIfNull(squares);
        _squares = squares.ToArray();
        _hits = new bool[_squares.Length];
    }

    public int Length => _squares.Length;

    public HitResult Hit(int row, int column)
    {
        for (int i = 0; i < _squares.Length; i++)
        {
            if (_squares[i].Position.Row == row && _squares[i].Position.Column == column)
            {
                _hits[i] = true;
                return _hits.All(h => h) ? HitResult.Sunken : HitResult.Hit;
            }
        }
        return HitResult.Missed;
    }
}
