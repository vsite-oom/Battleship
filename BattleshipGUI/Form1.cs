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

namespace BattleshipGUI
{
    public partial class Form1 : Form
    {
        Shipwright sw;
        Fleet humanFleet;
        Fleet neHumanFleet;
        List<Button> humanButtons = new List<Button>();
        List<Button> neHumanButtons = new List<Button>();

        public Form1()
        {
            InitializeComponent();

            for(int row = 0; row<10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Button button = new Button();
                    button.Size = new System.Drawing.Size(40, 40);
                    button.Location = new System.Drawing.Point((column + 1) * 40, (row + 1) * 40);

                    this.Controls.Add(button);
                    humanButtons.Add(button);
                }
            }

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Button button = new Button();
                    button.Size = new System.Drawing.Size(40, 40);
                    button.Location = new System.Drawing.Point(440 + (column + 1) * 40, (row + 1) * 40);

                    this.Controls.Add(button);
                    neHumanButtons.Add(button);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sw = new Shipwright(10, 10);
            List<int> shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            humanFleet = sw.CreateFleet(shipLengths);

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    Square sq = new Square(row, column);
                    foreach (Ship ship in humanFleet.Ships)
                    {
                        if(ship.Squares.Contains(sq))
                        {
                            humanButtons[row * 10 + column].BackColor = System.Drawing.Color.DarkGreen;
                            break;
                        }
                        else
                            humanButtons[row * 10 + column].BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }            
    }
}
