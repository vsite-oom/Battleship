using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public enum SqureState
    {
        Initial,
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
            SqureState = SqureState.Initial;
        }

        public readonly int Row;
        public readonly int Column;

        public SqureState SqureState { get; private set; }

        public void Mark(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    SqureState = SqureState.Missed;
                    break; 
                    case HitResult.Hit: 
                    SqureState = SqureState.Hit;
                    break;
                case HitResult.Sunk:
                    SqureState |= SqureState.Sunk;  
                    break;
                default:
                    Debug.Assert(false, "Unsuported hit result");
                    break;

            }
        }

        public bool Equals(Square other)
        {
            if (GetType() != other.GetType())
            {
                return false;
            }
            return Row == other.Row && Column == other.Column;
        }
        
       
        public override bool Equals(object obj) => Equals(obj as Square);

        public override int GetHashCode() => HashCode.Combine(Row, Column);
    }
}
