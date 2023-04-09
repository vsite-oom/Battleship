namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        readonly public int row;
        readonly public int column;
        public Square(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public bool Equals(Square other)
        {

            return row == other.row && column == other.column && GetType() == other.GetType();

        }
        public override bool Equals(object obj) => Equals(obj as Square);

        public override int GetHashCode() => HashCode.Combine(row, column);


    }
}

