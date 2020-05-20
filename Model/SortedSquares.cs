using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class SortedSquares: IEnumerable<Square>
    {
        public void Add(Square square)
        {
            squares.Add(square);
            squares = squares.OrderBy(s => s.row + s.column).ToList();
        }

        public void Clear()
        {
            squares.Clear();
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return squares.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return squares.GetEnumerator();
        }

        public int Length
        {
            get { return squares.Count(); }
        }

        /*public IEnumerable<Square> Squares
        {
            get { return squares; }
        }*/

        private List<Square> squares = new List<Square>();
    }
}
