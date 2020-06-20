using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI
{
    public partial class DrawFleetGrid : FlowLayoutPanel
    {

        public enum PlayerGridType
        {
            PLAYER,
            ENEMY
        }
        public DrawFleetGrid()
        {
            InitializeComponent();
            rules = RulesSingleton.Instance;
        }

        private Button DrawButton(int ID)
        {
            Button button = new Button();
            button.Name = "button" + ID;
            button.Width = Width / rules.Rows -7;
            button.Height = Height / rules.Columns -7;
            button.Visible = true;
            return button;
        }

        public void DrawGrid()
        {
            int numOfFields = rules.Columns * rules.Rows;
            for (int i = 0; i < numOfFields; ++i)
            {
                Controls.Add(DrawButton(i));
            }
        }

        private void DrawShips()
        {

        }

        public Shipwright Shipwright { get; set; }
        public Fleet Fleet { get; set; }
        public int Lines { get; set; }

        public PlayerGridType PlayerGrid { get; set; }


        private RulesSingleton rules;
        
    }
}
