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
        Gunner gunner;
        int humanShipsLeft;
        int neHumanShipsLeft;

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
                    button.Enabled = false;

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
                    button.Click += new System.EventHandler(this.battle_Click);
                    button.Name = row.ToString() + " " + column.ToString();
                    button.Enabled = false;

                    this.Controls.Add(button);
                    neHumanButtons.Add(button);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Restart";
            sw = new Shipwright(10, 10);
            List<int> shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            gunner = new Gunner(10, 10, shipLengths);
            humanFleet = sw.CreateFleet(shipLengths);
            neHumanFleet = sw.CreateFleet(shipLengths);
            humanShipsLeft = shipLengths.Count();
            neHumanShipsLeft = shipLengths.Count();
            textBox1.Visible = false;

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
            
            foreach ( Button button in neHumanButtons)
            {
                button.Enabled = true;
                button.BackColor = System.Drawing.Color.White;
            }
        } 
        
        private void battle_Click(object sender, EventArgs e)
        {
            if(humanShipsLeft != 0 && neHumanShipsLeft != 0)
            { 
                Button button = (Button)sender;
                Int32.TryParse(button.Name.Split(new Char[] { ' ' })[0], out int row);
                Int32.TryParse(button.Name.Substring(button.Name.IndexOf(' ') + 1), out int column);
            
                Square sq = new Square(row,column);
                HitResult result = neHumanFleet.Hit(sq);

                if (result == HitResult.Hit)
                    button.BackColor = System.Drawing.Color.Red;
                else if (result == HitResult.Sunken)
                {
                    neHumanShipsLeft--;
                    foreach (Ship ship in neHumanFleet.Ships)
                    {
                        if (ship.Squares.Contains(sq))
                        {
                            foreach (Square squareSunken in ship.Squares)
                            { 
                                neHumanButtons[squareSunken.Row * 10 + squareSunken.Column].BackColor = System.Drawing.Color.Black;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    button.BackColor = System.Drawing.Color.Gray;
                    button.Enabled = false;
                }

                if (neHumanShipsLeft == 0)
                { 
                    textBox1.Text = "We have a winner!";
                    textBox1.Visible = true;
                }
                else
                { 
                    sq = gunner.NextTarget();
                    result = humanFleet.Hit(sq);
                    gunner.ProcessHitResult(result);

                    if (result == HitResult.Hit)
                        humanButtons[sq.Row * 10 + sq.Column].BackColor = System.Drawing.Color.Red;
                    else if (result == HitResult.Sunken)
                    {
                        humanShipsLeft--;
                        foreach (Ship ship in humanFleet.Ships)
                        {
                            if (ship.Squares.Contains(sq))
                            {
                                foreach (Square squareSunken in ship.Squares)
                                {
                                    humanButtons[squareSunken.Row * 10 + squareSunken.Column].BackColor = System.Drawing.Color.Black;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        humanButtons[sq.Row * 10 + sq.Column].BackColor = System.Drawing.Color.Gray;
                    }

                    if (humanShipsLeft == 0)
                    { 
                        textBox1.Text="Loser!";
                        textBox1.Visible = true;
                    }
                }
            }
        }
    }
}