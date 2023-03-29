using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace Vsite.Oom.Battleship.Model
{
    using SquareSequence = IEnumerable<Square>;
    using Sequences = IEnumerable<IEnumerable<Square>>;
    public class Grid
    {
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            squares = new Square[Rows, Columns];
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    squares[r, c] = new Square(r, c);
                }
            }
        }

        public readonly int Rows;
        public readonly int Columns;


        private readonly Square[,] squares;


        public IEnumerable<Square> AvaiableSquares()
        {
            return squares.Cast<Square>();

        }

        public void RemoveSquare(int row, int column)
        {
            squares[row, column] = null;
        }
        public Sequences GetAvailableSequences(int lenght)
        {
            return GetAvailableHorizonatalSequences(lenght);
        }

        private Sequences GetAvailableHorizonatalSequences(int lenght)
        {
            var result = new List<SquareSequence>();
            for (int r = 0; r < Rows; ++r)
            {
                var queue = new LimitedQueue<Square>(lenght); 
                for (int c = 0; c < Columns; ++c)
                {
                    if (squares[r, c] != null)
                    {
                        queue.Enqueue(squares[r, c]);
                        
                        if (queue.Count == lenght)
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
            private Sequences GetAvailableVerticalSequences(int lenght)
            {
            var result = new List<SquareSequence>();
            return result;
            }
        }
    }


