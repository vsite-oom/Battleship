using Microsoft.Build.BuildEngine;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace ArrangeFleetGUI
{
    public partial class Battleship : Form
    {
        CheckBox[,] buttons;
        int rows = 10;
        int columns = 10;
        Color buttonColor = SystemColors.ControlLight;
        Color shipColor = Color.Brown;
        Fleet fleet = new Fleet();
        public Battleship()
        {
            InitializeComponent();

           CreateButtons();
         

        }

        private int CreateButtons()
        {
            buttons = new CheckBox[rows, columns];
            int buttonSize = Math.Min(panel1.Width / columns, panel1.Height / rows);

            int x0 = (panel1.Width - columns * buttonSize) / 2;
            int y = (panel1.Height - rows * buttonSize) / 2;
            for (int r = 0; r < rows; ++r)
            {
                int x = x0;
                for (int c = 0; c < columns; ++c)
                {
                    CheckBox button = new CheckBox
                    {
                        Top = y,
                        Left = x,
                        Width = buttonSize,
                        Height = buttonSize,
                        Appearance = Appearance.Button
                    };

                    buttons[r, c] = button;
                    panel1.Controls.Add(button);
                  
                    x += buttonSize;
                }
                y += buttonSize;
            }
            return buttonSize;
        }
        private void ResetButtons()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].Checked = false;
                    buttons[r, c].BackColor = buttonColor;
                }
            }
            List<Ship> ships = new List<Ship>(fleet.Ships);
            ships.Sort((s1, s2) => s2.Squares.Count());
            for (int i = 0; i < ships.Count(); i ++)
            {
                foreach (var square in ships[i].Squares)
                {
                    var button = buttons[square.Row, square.Column];
                  
                    button.BackColor = shipColor;
                }
            }
        }
        private void button101_Click(object sender, EventArgs e)
        {


            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            ResetButtons();
        }
    }
}

