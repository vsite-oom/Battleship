using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.oom.Battleship.Model;

namespace GUI
{
    public class Player
    {
        public Player()
        {
            fleet = new Fleet();
            Grid = new List<CheckBox>();
            shipsSunken = 0;
        }



        public Fleet fleet;
        public List<CheckBox> Grid;
        public int shipsSunken;
    }
}
