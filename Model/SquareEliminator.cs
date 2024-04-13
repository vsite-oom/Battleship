using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public class SquareEliminator
    {
        public IEnumerable<SquareCoordinate> ToEliminate(List<Square> shipSquares, int rows,int columns)
        {
            var first=shipSquares.First();
            var last=shipSquares.Last();
            int firstRow=first.Row;
            int firstCol=first.Column;
            int lastRow=last.Row;
            int lastCol=last.Column;
            if (firstRow > 0)
            {
                --firstRow;
            }
            if (firstCol > 0)
            {
                --firstCol;
            }
            if(lastRow < rows-1)
            {
                ++lastRow;
            }
            if (lastCol < columns)
            {
                ++lastCol;
            }
            var result=new List<SquareCoordinate>();
            for(int r = firstRow; r < lastRow; ++r)
            {
                for(int c = firstCol; c < lastCol; ++c)
                {
                    result.Add(new SquareCoordinate(r, c));
                }
            }
            return result;
        }
    }
}
