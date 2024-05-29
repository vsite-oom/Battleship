using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.OOM.Battleship.Model
{
    public enum Direction
    {
        Upwards,
        Downwards, 
        Left, 
        Right

    }
    public class ShotsGrid : Grid
    {
        public ShotsGrid(int rows, int columns) : base(rows, columns)
        {
        }

        protected override bool IsSquareAvailable(int row, int col)
        {
            return squares[row,col]?.State==SquareState.Intact;
        }

        public Square GetSquare(int row, int col)
        {
            return squares[row, col]!;
        }
        public void ChangeSquareState(int row, int col,SquareState newState) {
            squares[row, col]!.changeState(newState);
        }

        public IEnumerable<Square> GetSquaresInDirection(int row,int col,Direction dir) {
            List<Square> squares = new List<Square>();

            switch (dir)
            {
                case Direction.Upwards:
                {
                    for (int i = row - 1; i >= 0; --i)
                    {
                        if (GetSquare(i, col).State == SquareState.Intact)
                        {
                            squares.Add(GetSquare(i, col));
                        }
                        else
                        {
                            return squares;
                        }
                    }
                    return squares;
                }
                case Direction.Downwards:
                {
                    for (int i = row + 1; i <= Rows-1; ++i)
                    {
                        if (GetSquare(i, col).State == SquareState.Intact)
                        {
                            squares.Add(GetSquare(i, col));
                        }
                        else
                        {
                            return squares;
                        }
                    }
                    return squares;
                }
                case Direction.Left:
                {
                     for (int i = col - 1; i >= 0; --i)
                     {
                        if (GetSquare(row, i).State == SquareState.Intact)
                        {
                            squares.Add(GetSquare(row, i));
                        }
                        else
                        {
                            return squares;
                        }
                     }
                        return squares;
                }
                case Direction.Right:
                {
                    for (int i = col + 1; i <= Columns-1; ++i)
                    {
                        if (GetSquare(row, i).State == SquareState.Intact)
                        {
                            squares.Add(GetSquare(row, i));
                        }
                        else
                        {
                            return squares;
                        }
                    }
                    return squares;
                }
                default: { throw new Exception(); }
            }
        }
    }
}
