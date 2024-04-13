using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Ship
    {
        public Ship(IEnumerable<Square> squares)
        {
            this.squares = squares;
        }
        private readonly IEnumerable<Square> squares;
        public bool Contains(int row, int column)
        {
            return squares.FirstOrDefault(sq => sq.Row == row && sq.Column == column)!=null;
        }
    }
}
