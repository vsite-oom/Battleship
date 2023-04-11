namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public readonly int Row;
        public readonly int Column;

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }


        public bool Equals(Square other)
        {
            return GetType() == other.GetType() && this.Row == other.Row && this.Column == other.Column;
        }

        public override bool Equals(object obj) {
            return Equals(obj as Square);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
