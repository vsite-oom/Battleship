using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        public readonly Square?[,] Squares;
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Squares=new Square[rows, columns];
            for(int r = 0; r < rows; r++)
            {
                for(int c = 0; c < columns; c++)
                {
                    Squares[r,c]=new Square(r,c);
                }
            }
        }
        public IEnumerable<Square> squares
        {
            get { return Squares.Cast<Square>().Where(s=>s!=null); }
        }
        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {
            return GetHorizontalAvailablePlacements(length);
        }
        public IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result=new List<IEnumerable<Square>>();
            for(int r = 0; r < Rows;++r)
            {
                int counter = 0;
                for(int c = 0;c < Columns; ++c)
                {
                    if (Squares[r, c] != null)
                    {
                        
                        ++counter;
                        if(counter >= length)
                        {
                            List<Square> temp = new List<Square>();
                            for(int c1 = c - length+1; c1 <= c; ++c1)
                            {
                                temp.Add(Squares[r, c1]!);
                            }
                            result.Add(temp);
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            return result;
        }
    }
}
