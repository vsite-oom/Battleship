using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum HitResult
    {
        Missed,
        Hit,
        Sunk
    }
    public class Ship
    {
        
        private readonly IEnumerable<Square> Squares;
        public Ship(IEnumerable<Square> squares) {
            Squares = squares;
        }
        public bool Contains(int row, int column)
        {
            return Squares.FirstOrDefault(sq=> sq.Row==row && sq.Column==column) != null;
        }
        public HitResult Hit(int row,int col) {
        if (Contains(row,col)==false) return HitResult.Missed;
            throw new NotImplementedException();
        }
        

    }
}
