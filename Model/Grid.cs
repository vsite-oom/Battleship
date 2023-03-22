using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class Grid
    {
        public readonly int Rows;
        public readonly int Columns;

        private readonly Square[,] _squares;

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _squares = new Square[Rows, Columns];
            InitializeSquares();
        }

        private void InitializeSquares()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _squares[i, j] = new Square(i, j);
                }
            }
        }

        public IEnumerable<Square> AvailableSquares() => _squares.Cast<Square>().ToList();
    }
}
