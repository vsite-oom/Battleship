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


namespace RazmjestajFlote
{
    public partial class Form1 : Form
    {
        Square s = new Square(10, 10);
        public Form1()
        {
            InitializeComponent();
 
            PlayingGrid(playerGrid, 50, 10, 50);
            PlayingGrid(computerGrid, 550, 510, 550);
        }

        void PlayingGrid(Button[,] playingGrid, int positionC, int positionR, int positionB)
        {
            AddFields(playingGrid, positionB);
            AddColumnLabels(positionC);
            AddRowLabels(positionR);
        }
        void AddColumnLabels(int positionC)
        {
            int unicode = 65;
            for (int i = 0; i < s.Column; i++)
            {               
                char character = (char)unicode;
                unicode++;

                Label column_label = new Label() {
                    Width = 40,
                    Height = 40,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(i * 40),
                    Left = positionC + i * 40,
                    Top = 10,
                    Text = character.ToString(),
                    Font= new Font(Font.FontFamily.Name, 16),
                    TextAlign = ContentAlignment.MiddleCenter,
                };
            this.Controls.Add(column_label);
            }
        }
        void AddRowLabels(int positionB)
        {
            for (int j = 0; j < s.Row; j++)
            {
                Label row_label = new Label()
                {
                    Width = 40, 
                    Height = 40, 
                    BorderStyle = BorderStyle.FixedSingle, 
                    Location = new Point(j*40),
                    Top = 50 + j * 40,
                    Left = positionB,
                    Text = Convert.ToString(j + 1),
                    Font = new Font(Font.FontFamily.Name, 16),
                    TextAlign = ContentAlignment.MiddleCenter,  
                };
            this.Controls.Add(row_label);
            }
        }

        Button[,] playerGrid = new Button[10, 10];
        Button[,] computerGrid = new Button[10, 10];
        void AddFields(Button[,] buttons, int positionB)
        {
            for (int i = 0; i < s.Column; i++)
            {
                for (int j = 0; j < s.Row; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Width = 40;
                    buttons[i, j].Height = 40;
                    buttons[i, j].Tag = i + "," + j;
                    buttons[i, j].Location = new Point(positionB + i * 40, 50 + j * 40);                
                    buttons[i, j].Click += new EventHandler(GridButtonClick);
                    this.Controls.Add(buttons[i, j]);
                }
            }
        }
        Fleet playersFleet = new Fleet();
        Fleet computersFleet = new Fleet();
        Color fieldColor = SystemColors.ButtonFace;
        Color shipColor = Color.FromArgb(140, 192, 246);
        Color shipMissed = Color.FromArgb(200, 200, 200);
        Color shipHit = Color.FromArgb(80, 132, 186);
        Color shipSunken = Color.FromArgb(195, 35, 35);
        

        private void GridReset(Button[,] buttons)
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                       buttons[i, j].BackColor = fieldColor;
                }
            }
        }
        private void MarkPlayersShips(Button[,] buttons, Fleet fleet)
        {
            List<Ship> ships = new List<Ship>(fleet.Ships);
            for (int i = 0; i < ships.Count(); ++i)
            {
                foreach (var square in ships[i].Squares)
                {
                    if (buttons[square.Row, square.Column] != null)
                        buttons[square.Row, square.Column].BackColor = shipColor;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Shipwright shipwright = new Shipwright(10, 10);
            playersFleet = shipwright.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            computersFleet = shipwright.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });

            GridReset(playerGrid);
            GridReset(computerGrid);
            MarkPlayersShips(playerGrid, playersFleet);
        }
        private void GridButtonClick(object sender, System.EventArgs e)
        {
            Button button = sender as Button;
            string[] indexes = button.Tag.ToString().Split(',');
            Square field = new Square(Int32.Parse(indexes[0]), Int32.Parse(indexes[1]));
            HitResult hitResult = computersFleet.Hit(field);
            switch (hitResult)
            {
                case HitResult.Missed:
                    {
                        button.BackColor = shipMissed;
                        break;
                    }
                case HitResult.Hit:
                    {
                        button.BackColor = shipHit;
                        break;
                    }
                case HitResult.Sunken:
                    {
                        foreach (var square in computersFleet.Ships.Where(sq => sq.Squares.Contains(field)).SelectMany(sq => sq.Squares))
                        {
                            computerGrid[square.Row, square.Column].BackColor = shipSunken;
                        }
                        break;
                    }
            }
        }

    }
}
