using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Vsite.Oom.Battleship.Model
{
    public class SortedSquares : IEnumerable<Square>
    {
        private List<Square> squares = new List<Square>();

        public int Count { get { return squares.Count(); } }

        public void Add(Square square)
        {
            squares.Add(square);
            squares.Sort((a, b) => (a.Column + a.Row).CompareTo(b.Column + b.Row));
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
    }
}
