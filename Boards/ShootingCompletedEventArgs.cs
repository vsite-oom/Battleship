using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsite.Oom.Battleship.Model;

namespace Boards
{
    class ShootingCompletedEventArgs : EventArgs
    {
        public ShootingCompletedEventArgs(int r,int c, HitResult hr)
        {
            row = r;
            column = c;
            hitResult = hr;
        }
        
        public int row;
        public int column;
        public HitResult hitResult;
    }
}
