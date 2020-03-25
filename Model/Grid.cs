using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vsite.Oom.Battleship.Model
{
  public  class Grid
    {
        public Grid(int rows,int columns)
        {
            Rows = rows;
            Columns = columns;
        }

       public  IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
        {   
           throw new NotImplementedException();
        }
        public readonly int Rows;
        public readonly int Columns;
    }
}
