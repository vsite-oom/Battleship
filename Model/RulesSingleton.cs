using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.BattleShip.Model
{
    public class RulesSingleton
    {
        private static readonly RulesSingleton instance = new RulesSingleton();

        private RulesSingleton()
        {
            this.Rows = 10;
            this.Columns = 10;
            this.ShipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        }

        public static RulesSingleton Instance
        {
            get { return instance; }
        }

        public readonly int Rows;
        public readonly int Columns;
        public readonly int[] ShipLengths;
    }
}
