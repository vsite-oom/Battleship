using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Model;

namespace Model.Model;


public class FleetGrid :Grid
{

    public readonly int Rows;
    public readonly int Columns;

    private readonly Square?[,] squares;

    
   public FleetGrid(int rows, int columns) : base(rows, columns)
    {
        Rows = rows;
        Columns = columns;

        squares = new Square[Rows, Columns];

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                squares[r, c] = new Square(r, c);
            }
        }
    }

    public IEnumerable<Square> Squares
     public override IEnumerable<Square> Squares
    {
        get { return squares.Cast<Square>().Where(s => s != null); }
    }

    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        public void EliminateSquare(int row, int column)
    {
        return GetHorizontalAvailablePlacements(length);
    }

    private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

        for (int r = 0; r < Rows; r++)
        {
            int counter = 0;
            var queue = new LimitedQueue<Square>(length);
            for (int c = 0; c < Columns; c++)
            {
                if (squares[r, c] != null)
                {
                    ++counter;
                    if (counter >= length)
                        queue.Enqueue(squares[r, c]!);
                    if (queue.Count() == length)
                    {
                        List<Square> temp = new List<Square>();
                        for (int c1 = c - length + 1; c1 <= c; ++c1)
                        {
                            temp.Add(squares[r, c1]!);
                        }
                        result.Add(temp);
                        result.Add(queue.ToArray());
                    }
                }
                else
                {
                    counter = 0;
                    queue.Clear();
                }
            }
        }
        return result;
        squares[row, column] = null;
    }


}

  {
        List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

        for (int r = 0; r<Rows; ++r)
        {
            var queue = new LimitedQueue<Square>(length);
            for (int c = 0; c<Columns; ++c)
            {
                if (squares[r, c] != null)
                {
                    queue.Enqueue(squares[r, c]!);
                    if (queue.Count() == length)
                    {
                        result.Add(queue.ToArray());
                    }
                }
                else
{
    queue.Clear();
}
            }
        }
        return result;
    }

    private IEnumerable<IEnumerable<Square>> GetVerticalAvailablePlacements(int length)
{
    List<IEnumerable<Square>> result = new List<IEnumerable<Square>>();

    for (int c = 0; c < Columns; ++c)
    {
        var queue = new LimitedQueue<Square>(length);
        for (int r = 0; r < Rows; ++r)
        {
            if (squares[r, c] != null)
            {
                queue.Enqueue(squares[r, c]!);
                if (queue.Count() == length)
                {
                    result.Add(queue.ToArray());
                }
            }
            else
            {
                queue.Clear();
            }
        }
    }
    return result;
    squares[row, column] = null;
}

public void EleminateSquare(int row, int column)
    protected override bool IsSquareAvailable(int row, int column)
{
    squares[row, column] = null;
    return squares[row, column] != null;
}
}