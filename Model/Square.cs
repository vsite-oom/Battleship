using System;
using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            Hit = false;
            SquareState = SquareState.None;
        }

        public readonly int Row;
        public readonly int Column;

        public bool Hit { get; set; }
        public SquareState SquareState { get; private set; }

        public void SetState(ShipHitResult hitResult)
        {
            switch (hitResult)
            {
                case ShipHitResult.Missed:
                    SquareState = SquareState.Missed;
                    break;
                case ShipHitResult.Hit:
                    SquareState = SquareState.Hit;
                    break;
                case ShipHitResult.Sunken:
                    SquareState = SquareState.Sunken;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public bool Equals(Square other)
        {
            return other != null && Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            return Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }

        public static bool operator ==(Square first, Square second)
        {
            return Equals(first, second);
        }

        public static bool operator !=(Square first, Square second)
        {
            return !(first == second);
        }
    }
}
