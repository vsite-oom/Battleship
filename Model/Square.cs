using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class Square
public enum SquareState
{
    public readonly int Row;
public readonly int Column;
Intact,
    Eliminated,
    Missed,
    Hit,
    Sunken
}

public class Square
{
    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        SquareState = SquareState.Intact;
    }

    public readonly int Row;
    public readonly int Column;

    public void Hit()
    {
        hit = true;
        SquareState = SquareState.Hit;
    }

    public void ChangeState(SquareState newState)
    {
        if ((int)newState > (int)SquareState)
        {
            SquareState = newState;
        }
    }

    public bool IsHit => hit;
    public bool IsHit => (int)SquareState >= (int)SquareState.Hit;

    private bool hit = false;
}
    public SquareState SquareState { get; private set; }
}
