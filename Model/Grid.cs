using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public abstract class  Grid
    {
        public FleetGrid(int rows, int columns) : base(rows, columns)
        {
        }


    }
    public readonly int Rows;
    public readonly int Columns;



       protected readonly Square?[,] squares;
    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
    {
        return GetHorizontalAvailablePlacements(length).Concat(GetVerticalAvailablePlacements(length));
    }
    protected abstract bool IsSquareAvailable(int row, int column);

    private IEnumerable<IEnumerable<Square>> GetHorizontalAvailablePlacements(int length)
    {
        List<IEnumerable<Square>> result = new();

        for (int r = 0; r < Rows; r++)
        {
            var queue = new LimitedQueue<Square>(length);

            for (int c = 0; c < Columns; c++)
            {
                if (squares[r, c] != null)
                {
                    queue.Enqueue(squares[r, c]!);

                    if (queue.Count >= length)
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
                    if (queue.Count >= length)
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
}
