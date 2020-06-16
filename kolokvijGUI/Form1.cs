using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;
using static Vsite.Oom.Battleship.Model.Ship;

namespace ArrangeFleetGUI
{


    public partial class Battleship : Form
    {
        private bool myturn = true;
        CheckBox[,] buttons;
        CheckBox[,] buttons2;
        int rows = 10;
        int columns = 10;
        int rows2 = 10;
        int columns2 = 10;
        Color buttonColor = SystemColors.ControlLight;
        Color shipColor = Color.DarkTurquoise;
        static Fleet fleet = new Fleet();
        static Fleet fleet2 = new Fleet();


        private static int[] shiplengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };

        Gunner gunner1 = new Gunner(10, 10, shiplengths);
        Gunner gunner2 = new Gunner(10, 10, shiplengths);




        public Battleship()
        {
            InitializeComponent();

            int size = CreateButtons();
            AddLabels(size);
            AddLabels2(size);
        }

           private void panel1_Paint(object sender, PaintEventArgs e)
        {
       
        }

        private int CreateButtons()
        {
            buttons = new CheckBoxRowColumn[rows, columns];
            buttons2 = new CheckBoxRowColumn[rows2, columns2];
            int buttonSize = Math.Min(panel1.Width / columns, panel1.Height / rows);

            int x0 = (panel1.Width - columns * buttonSize) / 2;
            int y = (panel1.Height - rows * buttonSize) / 2;

            for (int r = 0; r < rows; ++r)
            {
                int x = x0;
                for (int c = 0; c < columns; ++c)
                {
                    CheckBoxRowColumn button = new CheckBoxRowColumn
                    {
                        Row = r,
                        Column = c,
                        Top = y,
                        Left = x,
                        Width = buttonSize,
                        Height = buttonSize,
                        Appearance = Appearance.Button
                    };
                    CheckBoxRowColumn button2 = new CheckBoxRowColumn
                    {
                        Row = r,
                        Column = c,
                        Top = y,
                        Left = x,
                        Width = buttonSize,
                        Height = buttonSize,
                        Appearance = Appearance.Button
                    };
                    button2.Click += Button2_Click;

                    buttons[r, c] = button;
                    buttons2[r, c] = button2;
                    panel1.Controls.Add(button);
                    panel2.Controls.Add(button2);
                    x += buttonSize;
                }
                y += buttonSize;
            }
            return buttonSize;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            CheckBoxRowColumn checkBox = (CheckBoxRowColumn)sender;
            if (myturn)
                myShot(checkBox.Row, checkBox.Column);
        }
        private void gameOver()
        {
            var computer = fleet.Ships.SelectMany(s => s.Squares).Any(s => s.SquareState != SquareState.Sunken);
            var me = fleet2.Ships.SelectMany(s => s.Squares).Any(s => s.SquareState != SquareState.Sunken);
            if (!computer)
            {
                MessageBox.Show("Fall back,our fleet is destroyed");
                this.Close();
            }
            if (!me)
            {
                MessageBox.Show("Good shooting,victory is ours");
                this.Close();
            }
        }
        public void EnemyFleet()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons2[r, c].Checked = false;
                    buttons2[r, c].BackColor = buttonColor;
                }
            }
            List<Ship> ships2 = new List<Ship>(fleet2.Ships);
            ships2.Sort((s1, s2) => s2.Squares.Count());
            for (int i = 0; i < ships2.Count(); i++)
            {

                foreach (var square2 in ships2[i].Squares)
                {
                    var button2 = buttons2[square2.Row, square2.Column];

                    button2.BackColor = buttonColor;
                }
            }
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
            for (int i = 0; i < ships.Count(); i++)
            {
                foreach (var square in ships[i].Squares)
                {
                    var button = buttons[square.Row, square.Column];

                    button.BackColor = shipColor;
                }

            }
        }
        private void AddLabels(int size)
        {
            int y = panel2.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label
                {
                    Top = y + 35,
                    Left = panel2.Left - size,
                    Width = size,
                    Height = size - 10,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = (r).ToString()
                };
                Controls.Add(label);
                y += size;
            }
            int x = panel2.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    Top = panel2.Top - size,
                    Left = x,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (c).ToString()
                };
                Controls.Add(label);
                x += size;
            }
        }
        private void AddLabels2(int size)
        {
            int y = panel1.Top;
            for (int r = 0; r < rows; ++r)
            {
                Label label = new Label
                {
                    Top = y + 35,
                    Left = panel1.Left - size,
                    Width = size,
                    Height = size - 10,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = (r).ToString()
                };
                Controls.Add(label);
                y += size;
            }
            int x = panel1.Left;
            for (int c = 0; c < columns; ++c)
            {
                Label label = new Label
                {
                    Top = panel1.Top - size,
                    Left = x,
                    Width = size,
                    Height = size,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = (c).ToString()
                };
                Controls.Add(label);
                x += size;
            }
        }
        private void button_Click(object sender, EventArgs e)
        {


            Shipwright sw = new Shipwright(rows, columns);
            fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            ResetButtons();
            Shipwright sw2 = new Shipwright(rows, columns);
            fleet2 = sw2.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            EnemyFleet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        public async void enemyShot()
        {
            myturn = false;



            await Task.Delay(800);

            ;
            int i = fleet.Ships.Count(s => s.Squares.All(sq => sq.SquareState == SquareState.Sunken));


            if (i != 10)
            {
                Square target = gunner2.NextTarget();
                HitResult hitResult = fleet.Hit(target);
                gunner2.ProcessHitResult(hitResult);
                markResult(target, hitResult, buttons);
                if (hitResult == HitResult.Hit || hitResult == HitResult.Sunken)
                {

                    enemyShot();

                }
                else
                    myturn = true;



                gameOver();
            }




        }



        private void markResult(Square target, HitResult hitResult, CheckBox[,] field)
        {

            switch (hitResult)
            {
                case HitResult.Missed:
                    field[target.Row, target.Column].BackColor = Color.Gray;
                    break;
                case HitResult.Hit:
                    field[target.Row, target.Column].BackColor = Color.Red;
                    break;
                case HitResult.Sunken:
                    field[target.Row, target.Column].BackColor = Color.Black;
                    MessageBox.Show("Ship destroyed");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
        public void myShot(int row, int col)
        {

            HitResult hitResult;

            Square sq = new Square(row, col);

            hitResult = fleet2.Hit(sq);
            markResult(sq, hitResult, buttons2);
            if (hitResult == HitResult.Missed)
            {

                enemyShot();
            }

            gameOver();
        }

     
    }
}



