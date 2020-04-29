using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        None,
        Misseed,
        Hit,
        Sunken
    }
    public class Square : IEquatable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            Hit = false;
            SquareState = SquareState.None;
        }

        public void SetState(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    SquareState = SquareState.Misseed;
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

        public readonly int Row;
        public readonly int Column;

        public SquareState SquareState { get; private set; }

        public bool Equals(Square other)
        {
            if (other == null)
            {
                return false;
            }
            return Row == other.Row && Column == other.Column;
        }

        public bool Hit
        {
            get; set;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Square)obj);
        }
        public override int GetHashCode()
        {
            return Row ^ Column;
        }

        public static bool operator ==(Square lhs, Square rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(Square lhs, Square rhs)
        {
            return !(lhs == rhs);
        }
    }
}