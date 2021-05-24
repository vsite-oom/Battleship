using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class SurroundingSquaresEliminator : ISquareEliminator
    {
        public SurroundingSquaresEliminator(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
        public IEnumerable<Square> ToEliminate(IEnumerable<Square> shipSquares)
        {
            
            List<Square> shipSquaresList = shipSquares.ToList();
            //shipSquaresList.Sort();
            int topLeftX = shipSquaresList[0].Row - 1;
            if (topLeftX < 0)
                topLeftX = 0;
            int topLeftXTemp = topLeftX;
            int topLeftY = shipSquaresList[0].Column - 1;
            if (topLeftY < 0)
                topLeftY = 0;
            int bottomRightX = shipSquaresList[shipSquaresList.Count - 1].Row + 1;
            if (bottomRightX >= rows || bottomRightX >= columns)
                bottomRightX = rows - 1;
            int bottomRightY = shipSquaresList[shipSquaresList.Count - 1].Column + 1;
            if (bottomRightY >= columns || bottomRightY >= columns)
                bottomRightY = columns - 1;

            List<Square> EliminatedSquares = new List<Square>();

            for (; topLeftY < bottomRightY + 1; ++topLeftY)
            {
                for (topLeftX = topLeftXTemp; topLeftX < bottomRightX + 1; ++topLeftX)
                {
                    EliminatedSquares.Add(new Square(topLeftX, topLeftY));
                }
            }

            return EliminatedSquares;
        }
        private readonly int rows;
        private readonly int columns;
    }
}
