using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;

    public class RecordGrid : Grid
    {
        public RecordGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override bool IsAvailable(Square square)
        {
            return square.SquareState == SquareState.Initial;
        }

        public void MarkSquare(int row, int column, HitResult hitResult)
        {
            squares[row, column].Mark(HitResult.Hit);
        }

        public SquareSequence GetAvailableSequence(Square from, Direction direction)
        {
            int row = from.Row;
            int endRow = row;
            int deltaRow = 0;
            if (direction == Direction.Upwards)
            {
                --row;
                deltaRow = -1;
                endRow = -1;
            }
            else if (direction == Direction.Downwards)
            {
                ++row;
                deltaRow = +1;
                endRow = Rows;
            }
            int column = from.Column;
            int endColumn = column;
            int deltaColumn = 0;
            if (direction == Direction.Leftwards)
            {
                --column;
                deltaColumn = -1;
                endColumn = -1;
            }
            else if (direction == Direction.Rightwards)
            {
                ++column;
                deltaColumn = +1;
                endColumn = Columns;
            }
            var result = new List<Square>();
            while (row != endRow || column != endColumn)
            {
                if (squares[row, column].SquareState != SquareState.Initial)
                {
                    break;
                }
                result.Add(squares[row, column]);
                row += deltaRow;
                column += deltaColumn;
            }
            return result;
        }

        public void Eliminate(int row, int column)
        {
            squares[row, column].Eliminate();
        }
    }
}
