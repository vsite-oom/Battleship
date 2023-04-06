namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public Square(int row,int column)
        {
            Row = row;
            Column = column;
        }
        public readonly int Row;
        public readonly int Column;

        public bool Equals(Square? other)
        {
            return Row == other?.Row && Column == other?.Column;
        }
        public override bool Equals(object? obj) => Equals(obj as Square);

        public override int GetHashCode() => HashCode.Combine(Row,Column);
    }
}
