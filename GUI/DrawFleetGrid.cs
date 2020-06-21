using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            Margin = new Padding(0);
            BackColor = Color.Aqua;
            buttons = new List<Button>();
            Turn = false;
        }

        private Button DrawButton(int ID)
        {
            Button button = new Button();
            button.Name = "button" + ID;
            button.Tag = ID;
            button.Width = Width / RulesSingleton.Instance.Rows - 7;
            button.Height = Height / RulesSingleton.Instance.Columns - 7;
            button.Visible = true;
            button.BackColor = Color.Aqua;
            if (playerGrid == PlayerGridType.PLAYER)
            {
                foreach (var fleetShip in Fleet.Ships)
                {
                    foreach (var fleetShipSquare in fleetShip.Squares)
                    {
                        if (fleetShipSquare.Row * 10 + fleetShipSquare.Column == ID)
                        {
                            button.BackColor = Color.DimGray;
                        }
                    }
                }
            }


            button.Parent = this;
            button.Click += new EventHandler(ButtonClick);
            buttons.Add(button);
            return button;
        }

        private void DrawGrid()
        {
            Fleet = Shipwright.CreateFleet(RulesSingleton.Instance.ShipLengths);
            int numOfFields = RulesSingleton.Instance.Columns * RulesSingleton.Instance.Rows;
            for (int i = 0; i < numOfFields; ++i)
            {
                Controls.Add(DrawButton(i));
            }
        }

        public void Init()
        {
            SquareTerminator = new SquareTerminator(RulesSingleton.Instance.Rows,
                                                    RulesSingleton.Instance.Columns);

            Shipwright = new Shipwright(RulesSingleton.Instance.Rows,
                             RulesSingleton.Instance.Columns,
                             SquareTerminator);

            DrawGrid();
        }


        void ButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            buttonClicked = (int)btn.Tag;
            base.OnClick(e);
        }


        public bool Turn { get; set; }
        public Shipwright Shipwright { get; set; }
        public Fleet Fleet { get; set; }
        public PlayerGridType playerGrid { get; set; }
        public ISquareTerminator SquareTerminator { get; set; }
        public int buttonClicked { get; private set; }
        
        
        public List<Button> buttons;
    }
}
