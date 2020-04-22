using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    using Placement = IEnumerable<Square>;

    public class Square
    {
        public Square(int row, int column)
        {
            this.row = row;
            this.column = column;
            Hit = false;
            
        }

        public bool Equals(Square other)
        {
            if(other == null)
            {
                return false;
            }
            return row == other.row && column == other.column;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if(obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Square)obj);
        }

        public bool Hit
        {
            get; set;
        }

        public override int GetHashCode()
        {
            return row ^ column;
        }

        public static bool operator==(Square lhs, Square rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator!=(Square lhs, Square rhs)
        {
            return !(lhs == rhs);
        }

        public readonly int row;
        public readonly int column;

        
    }
}
