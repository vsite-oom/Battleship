using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public enum Direction
{
    Upwards,
    Rightwards,
    Downwards,
    Leftwards
}

public class ShotsGrid : Grid
{
    public ShotsGrid(int rows, int columns) : base(rows, columns)
    {
        squares[row, column]!.ChangeState(newState);
            public IEnumerable<Square> GetSquaresInDirection(int row, int column, Direction upwards)
    {
        throw new NotImplementedException();
    }
    }

    protected override bool IsSquareAvailable(int row, int column)
    {
        throw new NotImplementedException();
        return squares[row, column]?.SquareState == SquareState.Intact;
    }

    public Square GetSquare(int row, int column);
    public void ChangeSquareState(int row, int column, SquareState newState)
    {
        return squares[row, column]!;
        squares[row, column]!.ChangeState(newState);
    }
}

