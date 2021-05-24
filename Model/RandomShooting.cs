using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RandomShooting : ITargetSelect
    {

        public RandomShooting(Grid grid, int shipLength)
        {
            this.grid = grid;
            this.shipLength = shipLength;

        }
        public Square NextTarget()
        {
            var allPlacements = grid.GetAvailablePlacements(shipLength);
            // create simple array of all squares
            var allCandidates = allPlacements.SelectMany(seq => seq);
            // create groups with individual squares
            var groups = allCandidates.GroupBy(sq => sq);
            // find number of squares in the largest group
            var maxCount = groups.Max(g => g.Count());
            // filter groups with count == maxCount
            var largestGroups = groups.Where(g => g.Count() == maxCount);
            //fetch keys from largestGroups
            var mostCommonSquares = largestGroups.Select(g => g.Key);
            if (mostCommonSquares.Count() == 1)
                return mostCommonSquares.First();
            int index = random.Next(mostCommonSquares.Count());
            return mostCommonSquares.ElementAt(index);

            // select 1 of squares using random
            // 1. calculate how many times each square appears in allPlacements (IEnumerable<IEnumerable<Square>>)
            // 2. find squares which appear most often
            // 3. from these squares select randomly one as target

            /* PRva implementacija
            SortedDictionary<Square, int> SquareFrequency = new SortedDictionary<Square, int>();
            for (int i = 0; i < allPlacements.Count(); ++i)
            {
                for (int j = 0; j < allPlacements.ElementAt(i).Count(); ++j)
                {
                    if (SquareFrequency.ContainsKey(allPlacements.ElementAt(i).ElementAt(j)))
                        SquareFrequency[allPlacements.ElementAt(i).ElementAt(j)]++;
                    else
                        SquareFrequency.Add(allPlacements.ElementAt(i).ElementAt(j), 1);
                }
            }
            

            int max = SquareFrequency.Values.Max();
            var SquaresWithMaxApperances = SquareFrequency.Where(pair => max.Equals(pair.Value)).Select(pair => pair.Key);
            int randBroj = random.Next(0, SquaresWithMaxApperances.Count());
            Square ChosenSquare = SquaresWithMaxApperances.ElementAt(randBroj);
            
            return ChosenSquare;
            */

            /* Druga implementacija
            int x, y;
            List<List<int>> SquareFrequency = new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            foreach (IEnumerable<Square> SSquare in allPlacements)
            {
                foreach (Square square in SSquare)
                {
                    x = square.Row;
                    y = square.Column;
                    SquareFrequency[x][y]++;
                }
            }

            int max = 0;
            for (int i = 0; i < SquareFrequency.Count(); ++i)
            {
                for (int j = 0; j < SquareFrequency[i].Count; ++j)
                {
                    if (SquareFrequency[i][j] > max)
                        max = SquareFrequency[i][j];
                }
            }

            int indexX = 0;
            int indexY = 0;
            foreach(List<int> Row in SquareFrequency)
            {
                if (Row.IndexOf(max) != -1)
                {
                    indexX = Row.IndexOf(max);
                    indexY = SquareFrequency.IndexOf(Row);
                }
            }

            Square FinalTarget = new Square(indexX, indexY);

            return FinalTarget;
            */
        }

        

        private Grid grid;
        private int shipLength;
        private Random random = new Random();
    }
}
