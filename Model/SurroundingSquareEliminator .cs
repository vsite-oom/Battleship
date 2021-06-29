using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquareEliminator : ISquareEliminator
    {
        public SurroundingSquareEliminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            var result = new List<Square>();

            if (!shipSquares.Any())
                return null;

            int minCol = shipSquares.Min(s => s.Column);
            int maxCol = shipSquares.Max(s => s.Column);
            int minRow = shipSquares.Min(s => s.Row);
            int maxRow = shipSquares.Max(s => s.Row);

            minRow = minRow > 0 ? minRow - 1 : 0;
            minCol = minCol > 0 ? minCol - 1 : 0;
            maxRow = maxRow < rows - 1 ? maxRow + 1 : maxRow;
            maxCol = maxCol < columns - 1  ? maxCol + 1 : maxCol;

            for (int i = minRow; i <= maxRow; i++)
            {
                for(int j = minCol; j <= maxCol; j++)
                {
                    Square sq = new Square(i,j);
                    result.Add(sq);
                }
            }

            return result;
        }

        private readonly int rows;
        private readonly int columns;
    }
}
