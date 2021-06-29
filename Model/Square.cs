using System;

namespace Vsite.Oom.Battleship.Model
{
    public enum SquareState
    {
        Default,
        Missed,
        Hit,
        Sunken
    }

    public struct Square : IEquatable<Square>
    {
        public readonly int row;
        public readonly int column;
        private SquareState squareState;

        public Square(int Row, int Column)
        {
            row = Row;
            column = Column;
            squareState = SquareState.Default;
        }

        public void SetSquareState(HitResult hitResult)
        {
            switch (hitResult)
            {
                case HitResult.Missed:
                    squareState = SquareState.Missed;
                    break;

                case HitResult.Hit:
                    squareState = SquareState.Hit;
                    break;

                case HitResult.Sunken:
                    squareState = SquareState.Sunken;
                    break;

                default:
                    squareState = SquareState.Default;
                    break;
            }
        }

        public SquareState SquareState
        {
            get { return squareState; }
        }

        public bool Equals(Square other)
        {
            return row == other.row && column == other.column;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            return row ^ column;
        }
    }
}