using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model
{
    public class RulesSingleton   //postoji 1 u app
    {
        private static readonly RulesSingleton instance = new RulesSingleton();
        private RulesSingleton()
        {
            //e.g. read data from config file
            Rows = 10;
            Columns = 10;
            ShipLentgths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        }

        public static RulesSingleton Instance { get { return instance; } }

        public readonly int  Rows;
        public readonly int Columns;
        public readonly int[] ShipLentgths;


    }
}
