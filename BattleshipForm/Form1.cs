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

namespace BattleshipForm
{
    public partial class Form1 : Form
    {
        Shipwright shpWrt;
        Fleet userFleet = new Fleet();
        Fleet cpuFleet = new Fleet();
        Gunner gunner;
        List<Button> userButtons = new List<Button>();
        List<Button> cpuButtons = new List<Button>();
        int userShipsToShoot;
        int cpuShipsToShoot;

        public Form1()
        {
            InitializeComponent();

            for (int row = 0; row < 10; ++row)
            {
                for (int column = 0; column < 10; ++column)
                {

                    Button button = new Button();
                    button.Location = new System.Drawing.Point(50 + column * 35, 60 + row * 35);
                    button.Size = new System.Drawing.Size(35, 35);
                    button.Enabled = false;
                    button.TabStop = false;
                    button.Name = row.ToString() + "-" + column.ToString();
                    userButtons.Add(button);
                    this.Controls.Add(button);

                    button = new Button();
                    button.Location = new System.Drawing.Point(550 + column * 35, 60 + row * 35);
                    button.Size = new System.Drawing.Size(35, 35);
                    button.Enabled = false;
                    button.TabStop = false;
                    button.Name = row.ToString() + "-" + column.ToString();
                    button.Click += new EventHandler(this.fieldClick);
                    cpuButtons.Add(button);
                    this.Controls.Add(button);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            NewGame();
            ((Button)sender).Text = "Reset";
            label2.Text = " ";
        }

        private void NewGame()
        {
            try
            {
                shpWrt = new Shipwright(10, 10);
                List<int> shipLenghts = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
                userFleet = shpWrt.CreateFleet(shipLenghts);
                cpuFleet = shpWrt.CreateFleet(shipLenghts);
                gunner = new Gunner(10, 10, shipLenghts);
                cpuShipsToShoot = shipLenghts.Count();
                userShipsToShoot = shipLenghts.Count();

                for (int row = 0; row < 10; ++row)
                {
                    for (int column = 0; column < 10; ++column)
                    {
                        foreach (Ship ship in userFleet.Ships)
                        {
                            if (ship.Squares.Contains(new Square(row, column)))
                            {
                                userButtons[row * 10 + column].BackColor = System.Drawing.Color.Green;
                                break;
                            }
                            else
                                userButtons[row * 10 + column].BackColor = System.Drawing.Color.White;
                        }
                    }
                }

                foreach (Button button in cpuButtons)
                {
                    button.Enabled = true;
                    button.BackColor = System.Drawing.Color.White;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                NewGame();
            }
        }

        private void fieldClick(object sender, EventArgs e)
        {
            if (cpuShipsToShoot != 0 && userShipsToShoot != 0)
            {
                userShoots(sender);

                if(cpuShipsToShoot==0)
                {
                    label2.Text = "WIN!!!!";
                }
                else
                {
                    //cpuShoots();
                }

                if (userShipsToShoot == 0)
                    label2.Text = "LOOSE";
            }
        }

        private void userShoots(object sender)
        {
            Button button = (Button)sender;
            button.Enabled = false;

            Int32.TryParse(button.Name.Split(new Char[] { '-' })[0], out int row);
            Int32.TryParse(button.Name.Substring(button.Name.IndexOf('-') + 1), out int column);
            Square square = new Square(row, column);

            HitResult result = cpuFleet.Hit(square);

            if (result == HitResult.Hit)
                button.BackColor = System.Drawing.Color.Red;
            else if (result == HitResult.Sunken)
            {
                foreach (Ship ship in cpuFleet.Ships)
                {
                    if (ship.Squares.Contains(square))
                    {
                        foreach (Square shipSquare in ship.Squares)
                        {
                            cpuButtons[shipSquare.Row * 10 + shipSquare.Column].BackColor = System.Drawing.Color.DarkKhaki;
                        }
                    }
                }
                --cpuShipsToShoot;
            }
            else
            {
                button.BackColor = System.Drawing.Color.Khaki;
            }
        }

        private void cpuShoots()
        {
            Square square = gunner.NextTarget();
            HitResult result = userFleet.Hit(square);
            

            if (result == HitResult.Hit)
                userButtons[square.Row * 10 + square.Column].BackColor = System.Drawing.Color.Red;
            else if (result == HitResult.Sunken)
            {
                foreach (Ship ship in userFleet.Ships)
                {
                    if (ship.Squares.Contains(square))
                    {
                        foreach (Square shipSquare in ship.Squares)
                        {
                            userButtons[shipSquare.Row * 10 + shipSquare.Column].BackColor = System.Drawing.Color.DarkKhaki;
                        }
                    }
                }
                --userShipsToShoot;
            }
            else
            {
                userButtons[square.Row * 10 + square.Column].BackColor = System.Drawing.Color.Khaki;
            }

            gunner.ProcessHitResult(result);
        }
    }
}
