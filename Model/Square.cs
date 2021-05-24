using System;
using System.Collections.Generic;
using System.Text;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Default,
        Missed,
        Hit,
        Sunken
    }
    public class Square : IEquatable<Square>, IComparable<Square>
    {
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            squareState = SquareState.Default;
        }
        public readonly int Row;
        public readonly int Column;

        private SquareState squareState;

        public void SetSquareState(HitResult hitResult)
        {
            switch(hitResult)
            {
                case HitResult.Missed:
                    this.squareState = SquareState.Missed;
                    break;
                case HitResult.Hit:
                    this.squareState = SquareState.Hit;
                    break;
                case HitResult.Sunken:
                    this.squareState = SquareState.Sunken;
                    break;
            }
        }

        public SquareState SquareState
        {
            get { return squareState; }
        }

        public bool Equals(Square other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;
            return Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            return Row ^ Column;
        }

        public int CompareTo(Square other)
        {
            if (this.Row == other.Row && this.Column == other.Column)
                return 0;
            return 1;
        }
    }
}
