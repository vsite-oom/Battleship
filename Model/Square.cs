using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public enum SquareState
        {
            Initial,
            Missed,
            Hit,
            Sank
        }

        public SquareState State { get; private set; }
        public readonly int Row;
        public readonly int Column;


        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            State = SquareState.Initial;
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

        public void Mark(HitResult hitResult)
        {
            switch(hitResult)
            {
                case HitResult.Hit:
                    State = SquareState.Hit;
                    break;
                case HitResult.Missed:
                    break;
                case HitResult.Sank:
                    State = SquareState.Sank;
                    break;
                default:
                    Debug.Assert(false, "Hit result not supported");
                    break;
            }
        }
    }
}
