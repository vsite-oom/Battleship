using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vsite.OOM.Battleship.Model;

namespace Vsite.OOM.Battleship.Model
{
    public class FleetGrid : Grid
    {
        public readonly int Rows;
        public readonly int Columns;
        private readonly Square?[,] squares;

        public FleetGrid(int rows, int columns) : base(rows, columns) 
        {
            
        }

        public override IEnumerable<Square> Squares
        {
            get
            {
                return squares.Cast<Square>().Where(s => s != null);
            }
        }

        public void EliminateSquare(int row, int column)
        {
            squares[row, column] = null;
        }

        protected override bool IsSquareAvailable(int row, int col)
        {
            return squares[row, col] != null;
        }
    }
}