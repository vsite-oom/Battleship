using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.oom.Battleship.Model
{
    public class Square : IEquatable<Square>
    {
        public Square(int rows, int columns)
        {

            Rows = rows;
            Columns = columns;
        }

        public readonly int Rows;
        public readonly int Columns;

        public bool Equals(Square other)
        {
            if (other == null)
                return false;
            return Rows == other.Rows && Columns == other.Columns;
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
            return Rows ^ Columns;
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
