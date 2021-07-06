using System;
using System.Diagnostics;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Default,
        Missed,
        Hit,
        Sunken
    };

    public struct Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row    = row;
            Column = column;
            SquareState = SquareState.Default;
        }

        public int Row;
        public int Column;
        public SquareState SquareState { get; set; }

        public bool Equals(Square rhs)
        {
            return Row == rhs.Row && Column == rhs.Column;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Square)obj);
        }
        
        public void SetSquareState(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    SquareState = SquareState.Missed;
                    break;
                case HitResult.Hit:
                    SquareState = SquareState.Hit;
                    break;
                case HitResult.Sunken:
                    SquareState = SquareState.Sunken;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }
    }
}


