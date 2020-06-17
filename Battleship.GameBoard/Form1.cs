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
        int columns =10;
        int rows = 10;
        int buttonSize = 40;
        int fleet1Position = 80;
        int fleet2Position = 510;
        List<Button> playerButtons = new List<Button>();
        List<Button> computerButtons = new List<Button>();
        bool human = true;
        bool computer = false;
        Fleet player;
        Fleet computerPlayer;
        Gunner gunner;
        int playerShipCount;
        int computerShipCount;

        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1000, 520);
            this.MaximumSize = new Size(1000, 520);
            this.MaximizeBox = false;
            GenerateGameBoard(rows, columns, fleet1Position, playerButtons, human);
            GenerateGameBoard(rows, columns, fleet2Position, computerButtons, computer);
        }
        public void GenerateGameBoard(int columns, int rows, int position, List<Button> buttons, bool isHuman)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonSize, buttonSize);
                    button.Location = new Point(position + (c + 1) * buttonSize, (r + 1) * buttonSize);
                   
                    if(isHuman == false)
                    {
                        button.Click += new System.EventHandler(this.Button_Click);
                        button.Name = r.ToString() + " " + c.ToString();
                    }
                   
                    button.Enabled = false;
                    this.Controls.Add(button);
                    button.BackColor = Color.LightBlue;
                    buttons.Add(button);                
                }
            }
        }
        private void CreateFleet_Click(object sender, EventArgs e)
        {
            List<int> shipLengths = new List<int> {2,2,2,2,3,3,3,4,4,5};
            Shipwright sw = new Shipwright(rows, columns);          
            gunner = new Gunner(rows, columns, shipLengths);          
            player = sw.CreateFleet(shipLengths);
            computerPlayer = sw.CreateFleet(shipLengths);
            playerShipCount = shipLengths.Count();
            computerShipCount = shipLengths.Count();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Square sq = new Square(r, c);
                   
                    foreach (Ship ship in player.Ships)
                    {
                        if (ship.Squares.Contains(sq))
                        {
                            playerButtons[r * 10 + c].BackColor = Color.Navy;
                        }
                    }

                }
            }
            
            foreach (Button button in computerButtons)
            {
                button.Enabled = true;
            }
        }
        public void SunkenShip(Square sq, Fleet fleet, List<Button> buttons)
        {
            foreach (Ship ship in fleet.Ships)
            {
                if (ship.Squares.Contains(sq))
                {
                    foreach (Square squareSunken in ship.Squares)
                    {
                        buttons[squareSunken.Row * 10 + squareSunken.Column].BackColor = Color.Orange;
                    }
                    break;
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (playerShipCount != 0 && computerShipCount != 0)
            {
                Button button = (Button)sender;
                Int32.TryParse(button.Name.Split(new Char[] { ' ' })[0], out int row);
                Int32.TryParse(button.Name.Substring(button.Name.IndexOf(' ') + 1), out int column);

                Square sq = new Square(row, column);
                HitResult result = computerPlayer.Hit(sq);

                if (result == HitResult.Hit)
                    button.BackColor = Color.Red;
                else if (result == HitResult.Sunken)
                {
                    computerShipCount--;
                    SunkenShip(sq, computerPlayer, computerButtons);
                }
                else
                {
                    button.BackColor = Color.White;
                    button.Enabled = false;
                }

                if (computerShipCount == 0)
                {
                    MessageBox.Show("WIN");
                }
                else
                {
                    sq = gunner.NextTarget();
                    result = player.Hit(sq);
                    gunner.ProcessHitResult(result);

                    if (result == HitResult.Hit)
                        playerButtons[sq.Row * 10 + sq.Column].BackColor = Color.Orange;
                    else if (result == HitResult.Sunken)
                    {
                        playerShipCount--;
                        SunkenShip(sq, player, playerButtons);
                    }
                    else
                    {
                        playerButtons[sq.Row * 10 + sq.Column].BackColor = Color.White;
                    }

                    if (playerShipCount == 0)
                    {
                        MessageBox.Show("LOSE");
                    }
                }
            }
        }
    }  
}