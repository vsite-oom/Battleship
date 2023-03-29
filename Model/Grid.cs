using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Vsite.Oom.Battleship.Model
    
{
    using SquareSequance = IEnumerable<Square>;
    using Sequances = IEnumerable<IEnumerable<Square>>;
    public class Grid
    {
        readonly public int rows;
        readonly public int columns;
        private Square[,] squares;
        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            squares = new Square[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < columns; c++)
                {
                    squares[r, c]=new Square(r, c);
                     
                }
            }
        }
        public IEnumerable<Square> AvailableSquares()
        {
            return squares.Cast<Square>();
        }

        public Sequances GetAvaliableSequences(int length) {

            return GetAvaliableHorizontalSequances(length).Concat(GetAvaliableVerticalSequances(length));



        }
        private Sequances  GetAvaliableHorizontalSequances(int length)
        {
            var result=new List<SquareSequance>();
            for(int r=0; r < rows; r++)
            {
               
                var queue = new LimitedQueue<Square>(length);
                for(int c=0; c < columns; c++)
                {
                    if (squares[r, c] != null)
                    {
                        
                        queue.Enqueue(squares[r, c]);
                        if(queue.Count == length)
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
        private Sequances GetAvaliableVerticalSequances(int length)
        {
            var result = new List<SquareSequance>();
            for (int c = 0; c < columns; c++) 
            {
                var queue = new LimitedQueue<Square>(length);
                for (int r = 0; r < rows; r++)
                {
                    if (squares[r, c] != null)
                    {
                        queue.Enqueue(squares[r, c]);
                        if (queue.Count == length)
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
        public void RemoveSquare(int row,int column) { squares[row, column] = null; }
    }
}
