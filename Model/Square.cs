using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            squareState = SquareState.Default;
        }

        public void SetState(SquareState newState)
        {
            squareState = newState;
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

        public static bool operator ==(Square sq1, Square sq2)
        {
            return sq1.Equals(sq2);
        }

        public static bool operator !=(Square sq1, Square sq2)
        {
            return !(sq1 ==sq2);
        }

        public SquareState SquareState
        {
            get { return squareState; }
        }

        public readonly int Row;
        public readonly int Column;

        private SquareState squareState;
    }
}
