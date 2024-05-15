using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vsite.oom.battleship.model;

namespace Vsite.Oom.Battleship.Model
{
    public class FleetGrid : Grid
    {
        public FleetGrid(int rows, int columns) : base(rows, columns)
        {
        }

        public readonly int Rows;
        public readonly int Columns;

        private readonly Square?[,] squares;

        public override IEnumerable<Square> Squares
        {
            get { return squares.Cast<Square>().Where(s => s != null); }
        }

        public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {

            return GetHorizontalAvailablePlacements(length);
        }

        private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
        {
            List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();
            for(int r = 0; r < Rows; r++)
            {
                int counter = 0;
                for(int c = 0; c < Columns; c++)
                {
                    if (squares[r, c] != null)
                    {
                        ++counter;
                        if(counter >= length)
                        {
                            List<Square> temp = new List<Square>();
                            for (int c1 = c - length + 1; c1 <= c; ++c1)
                            {
                                temp.Add(squares[r, c1]!);
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
        public void EliminateSquare(int row, int column)
        {
            squares[row, column] = null;
        }
        protected override bool isSquareAvailable(int row, int column)
        {
            return squares[row, column] != null;
        }
    }
}
