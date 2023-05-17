using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return square.State == Square.SquareState.Initial;
        }

        public SquareSequence GetAvailableSequence(Square reference, Direction direction)
        {
            SquareSequence res;
            switch (direction)
            {
                case Direction.Upwards:
                    res = squares.Cast<Square>().Where(sq => sq.State == Square.SquareState.Initial && sq.Row < reference.Row && sq.Column == reference.Column);
                    res = res.OrderBy(sq => sq.Row + sq.Column).Reverse();
                    break;
                case Direction.Downwards:
                    res = squares.Cast<Square>().Where(sq => sq.State == Square.SquareState.Initial && sq.Row > reference.Row && sq.Column == reference.Column);
                    res.OrderBy(sq => sq.Row + sq.Column);
                    break;
                case Direction.Leftwards:
                    res = squares.Cast<Square>().Where(sq => sq.State == Square.SquareState.Initial && sq.Row == reference.Row && sq.Column < reference.Column);
                    res = res.OrderBy(sq => sq.Row + sq.Column).Reverse();
                    break;
                case Direction.Rightwards:
                    res = squares.Cast<Square>().Where(sq => sq.State == Square.SquareState.Initial && sq.Row == reference.Row && sq.Column > reference.Column);
                    res.OrderBy(sq => sq.Row + sq.Column);
                    break;
                default:
                    Debug.Assert(false, "Direction not supported");
                    return AvailableSquares();
            }
            return res;
        }

        public void MarkSquare(int row, int col, HitResult hitResult)
        {
            squares[row, col].Mark(hitResult);
        }

        public void Eliminate(int row, int col)
        {
            squares[row, col].Eliminate();
        }
    }
}
