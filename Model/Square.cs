namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public readonly int Row;
        public readonly int Column;

        public bool Equals(Square other)
        {
            if (GetType() != other.GetType())
                return false;
            return this.Row == other.Row && this.Column == other.Column;
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
