using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Initial,
        Eliminated,
        Missed,
        Hit,
        Sunk
    }

    public class Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            SquareState = SquareState.Initial;
        }

        public readonly int Row;
        public readonly int Column;

        public SquareState SquareState { get; private set; }

        public void Mark(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    SquareState = SquareState.Missed;
                    break;
                case HitResult.Hit:
                    SquareState = SquareState.Hit;
                    break;
                case HitResult.Sunk:
                    SquareState = SquareState.Sunk;
                    break;
                default:
                    Debug.Assert(false, "Unsupported hit result");
                    break;
            }
        }

        public void Eliminate()
        {
            SquareState = SquareState.Eliminated;
        }

        public bool Equals(Square other)
        {
            if (GetType() != other.GetType())
                return false;

            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj) => Equals(obj as Square);

        public override int GetHashCode() => HashCode.Combine(Row, Column);
    }
}
