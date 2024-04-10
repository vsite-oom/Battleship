using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public Grid(int rows, int columns)  
        { 
            Rows = rows;
            Columns = columns;

            squares = new Square[Rows,Columns];

            for (int r=0; r<Rows; r++)
            {
                for(int c=0; c<Columns; c++)
                {
                    squares[r,c] = new Square(r,c);
                }
            }
        
        }


        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] squares;

        public IEnumerable<Square> Squares // sakrivamo implementaciju
        {

            get { return squares.Cast<Square>().Where(s => s != null); }  // dobivamo squareove u mrezi bez Null!

        }

        public IEnumerable<IEnumerable<Square>> GetAvaliablePlacements(int length)
        {
            return GetHorizontalAvaliablePlacements(length);

            // test .Aggregate(GetHorizontalAvaliablePlacements) za spajanje dvije kolekcije
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvaliablePlacements(int length)
        {
            List<IEnumerable<Square>>result=new List<IEnumerable<Square>>();

            for (int r=0; r<Rows; r++)
            {
                var queue = new  LimitedQueue<Square>(length);
                for (int c=0; c<Columns; c++)
                {
                    if (squares[r,c] != null)
                    {
                        queue.Enqueue(squares[r, c]!);
                        if (queue.Count() == length)
                        {
                            result.Add(queue.ToArray());
                        }
                    }
                    else
                    {
                        queue.Clear();
                    }
                }
            }
            return result;
        }
    }
}
